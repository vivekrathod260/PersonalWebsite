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
}
