import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Profile } from '../../core/models/portfolio.models';

@Component({
  selector: 'app-hero',
  standalone: true,
  imports: [CommonModule],
  template: `
    <section class="min-h-screen flex items-center relative overflow-hidden pt-16">
      <!-- Background gradient orbs -->
      <div class="absolute top-1/4 -left-32 w-96 h-96 bg-primary/20 rounded-full blur-3xl animate-float"></div>
      <div class="absolute bottom-1/4 -right-32 w-96 h-96 bg-secondary/20 rounded-full blur-3xl animate-float" style="animation-delay: 3s;"></div>

      <div class="section-container relative z-10">
        <div class="grid lg:grid-cols-2 gap-12 items-center">
          <div class="animate-fade-in">
            <p class="text-primary font-mono text-sm mb-4">Hello, I'm</p>
            <h1 class="text-4xl sm:text-5xl lg:text-6xl font-bold mb-4">
              {{ profile?.name || '' }}
            </h1>
            <h2 class="text-2xl sm:text-3xl text-gray-400 font-light mb-6">
              {{ profile?.title || '' }}
            </h2>
            <p class="text-gray-400 text-lg leading-relaxed mb-8 max-w-lg">
              {{ profile?.intro || '' }}
            </p>
            <div class="flex flex-wrap gap-4">
              <a href="#contact" class="btn-primary">{{ profile?.ctaPrimary || 'Get In Touch' }}</a>
              <a [href]="profile?.resumeUrl" target="_blank" class="btn-outline" *ngIf="profile?.resumeUrl">
                {{ profile?.ctaSecondary || 'Download Resume' }}
              </a>
            </div>
          </div>
          <div class="hidden lg:flex justify-center animate-slide-up" *ngIf="profile?.imageUrl">
            <div class="relative">
              <div class="w-80 h-80 rounded-full overflow-hidden border-2 border-primary/30 shadow-2xl shadow-primary/20">
                <img [src]="profile?.imageUrl" [alt]="profile?.name" class="w-full h-full object-cover" />
              </div>
              <div class="absolute inset-0 rounded-full bg-gradient-to-tr from-primary/10 to-transparent"></div>
            </div>
          </div>
        </div>
      </div>
    </section>
  `,
})
export class HeroComponent {
  @Input() profile: Profile | null = null;
}
