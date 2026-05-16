import { Component, Input, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { Profile, Social, ContactForm } from '../../core/models/portfolio.models';
import { PortfolioService } from '../../core/services/portfolio.service';

@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css'],
})
export class ContactComponent {
  @Input() profile: Profile | null = null;
  @Input() socials: Social[] = [];

  private portfolioService = inject(PortfolioService);

  form: ContactForm = { name: '', email: '', subject: '', message: '' };
  sending = signal(false);
  successMessage = signal('');
  errorMessage = signal('');
  toastMessage = signal('');
  toastType = signal<'success' | 'error' | ''>('');
  private toastTimer: any = null;

  constructor(private sanitizer: DomSanitizer) {}

  onSubmit() {
    // Trim values
    this.form.name = this.form.name?.trim() || '';
    this.form.email = this.form.email?.trim() || '';
    this.form.subject = this.form.subject?.trim() || '';
    this.form.message = this.form.message?.trim() || '';

    if (!this.isFormValid()) {
      this.errorMessage.set('Please fill in all required fields with a valid email.');
      this.showToast('Please complete the form with a valid email.', 'error');
      return;
    }

    this.sending.set(true);
    this.errorMessage.set('');
    this.successMessage.set('');

    this.portfolioService.submitContact(this.form).subscribe({
      next: (res) => {
        this.successMessage.set(res.message);
        this.showToast(res.message || 'Message sent successfully!', 'success');
        this.form = { name: '', email: '', subject: '', message: '' };
        this.sending.set(false);
      },
      error: () => {
        this.errorMessage.set('Failed to send message. Please try again.');
        this.showToast('Failed to send message. Please try again.', 'error');
        this.sending.set(false);
      },
    });
  }

  iconIsUrl(icon?: string) {
    if (!icon) return false;
    return /^(https?:)?\/\//.test(icon) || icon.startsWith('/');
  }

  isSvgContent(icon?: string) {
    if (!icon) return false;
    return icon.trim().startsWith('<svg') || icon.includes('<svg');
  }

  sanitizedIcon(icon?: string): SafeHtml | null {
    if (!icon) return null;
    return this.sanitizer.bypassSecurityTrustHtml(icon);
  }

  validEmail(email?: string) {
    if (!email) return false;
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return re.test(email);
  }

  isFormValid() {
    return Boolean(
      this.form.name && this.form.name.trim() &&
      this.form.email && this.form.email.trim() && this.validEmail(this.form.email) &&
      this.form.message && this.form.message.trim()
    );
  }

  showToast(message: string, type: 'success' | 'error') {
    // clear existing timer
    if (this.toastTimer) {
      clearTimeout(this.toastTimer);
      this.toastTimer = null;
    }
    this.toastMessage.set(message || '');
    this.toastType.set(type || '');
    // auto-hide after 4 seconds
    this.toastTimer = setTimeout(() => {
      this.toastMessage.set('');
      this.toastType.set('');
      this.toastTimer = null;
    }, 4000);
  }
}
