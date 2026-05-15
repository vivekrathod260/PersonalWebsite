import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Profile, Social } from '../../core/models/portfolio.models';

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css'],
})
export class FooterComponent {
  @Input() profile: Profile | null = null;
  @Input() socials: Social[] = [];
  currentYear = new Date().getFullYear();
}
