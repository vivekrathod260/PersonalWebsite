import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Profile, Social } from '../../core/models/portfolio.models';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent {
  @Input() profile: Profile | null = null;
  @Input() socials: Social[] = [];
  menuOpen = false;
  // Smooth-scroll to section with offset for fixed header
  scrollTo(id: string) {
    const el = document.getElementById(id);
    if (!el) return;
    const headerOffset = 70; // adjust if your navbar height differs
    const elementPosition = el.getBoundingClientRect().top + window.pageYOffset;
    const offsetPosition = elementPosition - headerOffset;
    window.scrollTo({ top: offsetPosition, behavior: 'smooth' });
  }

  onAnchorClick(event: Event, id: string) {
    event.preventDefault();
    this.menuOpen = false;
    this.scrollTo(id);
  }
}
