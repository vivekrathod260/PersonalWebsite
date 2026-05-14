import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Testimonial } from '../../core/models/portfolio.models';

@Component({
  selector: 'app-testimonials',
  standalone: true,
  imports: [CommonModule],
  template: `
    <section id="testimonials">
      <div class="section-container">
        <h2 class="section-title">Client <span class="gradient-text">Testimonials</span></h2>
        <p class="section-subtitle">What people say about me</p>

        <div class="grid md:grid-cols-2 lg:grid-cols-3 gap-6">
          <div *ngFor="let testimonial of testimonials"
               class="glass p-6 hover:border-primary/30 transition-all duration-300">
            <!-- Quote icon -->
            <svg class="w-8 h-8 text-primary/40 mb-4" fill="currentColor" viewBox="0 0 24 24">
              <path d="M14.017 21v-7.391c0-5.704 3.731-9.57 8.983-10.609l.995 2.151c-2.432.917-3.995 3.638-3.995 5.849h4v10H14.017zM0 21v-7.391c0-5.704 3.731-9.57 8.983-10.609l.995 2.151C7.546 6.068 5.983 8.789 5.983 11h4v10H0z"/>
            </svg>
            <p class="text-gray-300 text-sm leading-relaxed mb-6">{{ testimonial.review }}</p>
            <div class="flex items-center gap-3">
              <div class="w-10 h-10 rounded-full overflow-hidden bg-dark-700" *ngIf="testimonial.imageUrl">
                <img [src]="testimonial.imageUrl" [alt]="testimonial.name" class="w-full h-full object-cover" />
              </div>
              <div class="w-10 h-10 rounded-full bg-gradient-to-br from-primary to-secondary flex items-center justify-center text-white font-bold text-sm"
                   *ngIf="!testimonial.imageUrl">
                {{ testimonial.name.charAt(0) }}
              </div>
              <div>
                <p class="text-white font-medium text-sm">{{ testimonial.name }}</p>
                <p class="text-gray-500 text-xs">{{ testimonial.designation }}, {{ testimonial.company }}</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  `,
})
export class TestimonialsComponent {
  @Input() testimonials: Testimonial[] = [];
}
