import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Profile, Social } from '../../core/models/portfolio.models';

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [CommonModule],
  template: `
    <footer class="border-t border-white/5">
      <div class="max-w-6xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <div class="flex flex-col md:flex-row items-center justify-between gap-4">
          <p class="text-gray-500 text-sm">
            &copy; {{ currentYear }} {{ profile?.name || 'Portfolio' }}. All rights reserved.
          </p>
          <div class="flex items-center gap-4">
            <a *ngFor="let social of socials" [href]="social.url" target="_blank"
               class="text-gray-500 hover:text-primary transition-colors text-sm">
              {{ social.platform }}
            </a>
          </div>
        </div>
      </div>
    </footer>
  `,
})
export class FooterComponent {
  @Input() profile: Profile | null = null;
  @Input() socials: Social[] = [];
  currentYear = new Date().getFullYear();
}
