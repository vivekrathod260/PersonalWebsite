import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Profile, Social } from '../../core/models/portfolio.models';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule],
  template: `
    <nav class="fixed top-0 left-0 right-0 z-50 glass border-b border-white/5">
      <div class="max-w-6xl mx-auto px-4 sm:px-6 lg:px-8">
        <div class="flex items-center justify-between h-16">
          <a href="#" class="text-xl font-bold gradient-text">
            {{ profile?.name || 'Portfolio' }}
          </a>
          <div class="hidden md:flex items-center space-x-8">
            <a href="#about" class="text-gray-300 hover:text-white transition-colors text-sm">About</a>
            <a href="#experience" class="text-gray-300 hover:text-white transition-colors text-sm">Experience</a>
            <a href="#projects" class="text-gray-300 hover:text-white transition-colors text-sm">Projects</a>
            <a href="#skills" class="text-gray-300 hover:text-white transition-colors text-sm">Skills</a>
            <a href="#testimonials" class="text-gray-300 hover:text-white transition-colors text-sm">Testimonials</a>
            <a href="#contact" class="btn-primary text-sm !px-4 !py-2">Contact</a>
          </div>
          <button (click)="menuOpen = !menuOpen" class="md:hidden text-gray-300">
            <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path *ngIf="!menuOpen" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 12h16M4 18h16"/>
              <path *ngIf="menuOpen" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"/>
            </svg>
          </button>
        </div>
        <div *ngIf="menuOpen" class="md:hidden pb-4 space-y-2">
          <a href="#about" (click)="menuOpen=false" class="block py-2 text-gray-300 hover:text-white">About</a>
          <a href="#experience" (click)="menuOpen=false" class="block py-2 text-gray-300 hover:text-white">Experience</a>
          <a href="#projects" (click)="menuOpen=false" class="block py-2 text-gray-300 hover:text-white">Projects</a>
          <a href="#skills" (click)="menuOpen=false" class="block py-2 text-gray-300 hover:text-white">Skills</a>
          <a href="#contact" (click)="menuOpen=false" class="block py-2 text-gray-300 hover:text-white">Contact</a>
        </div>
      </div>
    </nav>
  `,
})
export class NavbarComponent {
  @Input() profile: Profile | null = null;
  @Input() socials: Social[] = [];
  menuOpen = false;
}
