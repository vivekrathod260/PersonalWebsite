import { Component, Input, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
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

  onSubmit() {
    if (!this.form.name || !this.form.email || !this.form.message) {
      this.errorMessage.set('Please fill in all required fields.');
      return;
    }

    this.sending.set(true);
    this.errorMessage.set('');
    this.successMessage.set('');

    this.portfolioService.submitContact(this.form).subscribe({
      next: (res) => {
        this.successMessage.set(res.message);
        this.form = { name: '', email: '', subject: '', message: '' };
        this.sending.set(false);
      },
      error: () => {
        this.errorMessage.set('Failed to send message. Please try again.');
        this.sending.set(false);
      },
    });
  }
}
