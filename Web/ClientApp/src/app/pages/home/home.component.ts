import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { forkJoin } from 'rxjs';
import { PortfolioService } from '../../core/services/portfolio.service';
import {
  Profile,
  Project,
  Experience,
  SkillCategory,
  Testimonial,
  Social,
} from '../../core/models/portfolio.models';
import { HeroComponent } from '../../components/hero/hero.component';
import { AboutComponent } from '../../components/about/about.component';
import { ExperienceComponent } from '../../components/experience/experience.component';
import { ProjectsComponent } from '../../components/projects/projects.component';
import { SkillsComponent } from '../../components/skills/skills.component';
import { TestimonialsComponent } from '../../components/testimonials/testimonials.component';
import { ContactComponent } from '../../components/contact/contact.component';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { FooterComponent } from '../../components/footer/footer.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    NavbarComponent,
    HeroComponent,
    AboutComponent,
    ExperienceComponent,
    ProjectsComponent,
    SkillsComponent,
    TestimonialsComponent,
    ContactComponent,
    FooterComponent,
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  private portfolioService = inject(PortfolioService);

  profile = signal<Profile | null>(null);
  projects = signal<Project[]>([]);
  experiences = signal<Experience[]>([]);
  skills = signal<SkillCategory[]>([]);
  testimonials = signal<Testimonial[]>([]);
  socials = signal<Social[]>([]);
  loading = signal(true);

  ngOnInit() {
    this.loading.set(true);
    // Load all portfolio data in parallel and update signals once complete
    // so UI can show a single loading state and avoid flicker.
    forkJoin({
        profile: this.portfolioService.getProfile(),
        projects: this.portfolioService.getProjects(),
        experiences: this.portfolioService.getExperiences(),
        skills: this.portfolioService.getSkills(),
        testimonials: this.portfolioService.getTestimonials(),
        socials: this.portfolioService.getSocials(),
      }).subscribe({
        next: res => {
          this.profile.set(res.profile);
          this.projects.set(res.projects);
          this.experiences.set(res.experiences);
          this.skills.set(res.skills);
          this.testimonials.set(res.testimonials);
          this.socials.set(res.socials);
          this.loading.set(false);
        },
        error: err => {
          console.error('Failed to load portfolio data', err);
          // still hide loading to allow the app to render and show errors/placeholders
          this.loading.set(false);
        }
      });
    });
  }
}
