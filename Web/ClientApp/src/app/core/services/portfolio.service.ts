import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  Profile,
  Project,
  Experience,
  SkillCategory,
  Testimonial,
  Social,
  ContactForm,
} from '../models/portfolio.models';

@Injectable({ providedIn: 'root' })
export class PortfolioService {
  private http = inject(HttpClient);
  private baseUrl = '/api/portfolio';

  getProfile(): Observable<Profile> {
    return this.http.get<Profile>(`${this.baseUrl}/profile`);
  }

  getProjects(): Observable<Project[]> {
    return this.http.get<Project[]>(`${this.baseUrl}/projects`);
  }

  getFeaturedProjects(): Observable<Project[]> {
    return this.http.get<Project[]>(`${this.baseUrl}/projects/featured`);
  }

  getExperiences(): Observable<Experience[]> {
    return this.http.get<Experience[]>(`${this.baseUrl}/experiences`);
  }

  getSkills(): Observable<SkillCategory[]> {
    return this.http.get<SkillCategory[]>(`${this.baseUrl}/skills`);
  }

  getTestimonials(): Observable<Testimonial[]> {
    return this.http.get<Testimonial[]>(`${this.baseUrl}/testimonials`);
  }

  getSocials(): Observable<Social[]> {
    return this.http.get<Social[]>(`${this.baseUrl}/socials`);
  }

  submitContact(form: ContactForm): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(`${this.baseUrl}/contact`, form);
  }
}
