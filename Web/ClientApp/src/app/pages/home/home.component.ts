import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
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

  ngOnInit() {
    this.portfolioService.getProfile().subscribe(data => this.profile.set(data));
    this.portfolioService.getProjects().subscribe(data => this.projects.set(data));
    this.portfolioService.getExperiences().subscribe(data => this.experiences.set(data));
    this.portfolioService.getSkills().subscribe(data => this.skills.set(data));
    this.portfolioService.getTestimonials().subscribe(data => this.testimonials.set(data));
    this.portfolioService.getSocials().subscribe(data => this.socials.set(data));
  }
}
