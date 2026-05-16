import { Component, Input, OnChanges, SimpleChanges, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Project } from '../../core/models/portfolio.models';

@Component({
  selector: 'app-projects',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css'],
})
export class ProjectsComponent implements OnChanges, OnDestroy {
  @Input() projects: Project[] = [];

  // track current image index per project id
  currentIndexMap: Record<string, number> = {};
  // track rotation timers per project id
  intervalMap: Record<string, any> = {};
  // touch swipe tracking
  touchStartMap: Record<string, number> = {};
  touchDeltaMap: Record<string, number> = {};

  // auto-rotate delay (ms)
  rotationDelay = 5000;

  ngOnChanges(changes: SimpleChanges) {
    if (changes['projects']) {
      // initialize indices and timers for each project
      this.stopAllTimers();
      (this.projects || []).forEach(p => {
        this.currentIndexMap[p.id] = 0;
        this.startTimerForProject(p);
      });
    }
  }

  ngOnDestroy() {
    this.stopAllTimers();
  }

  startTimerForProject(p: Project) {
    if (!p.images || p.images.length <= 1) return;
    // clear existing
    this.clearTimer(p.id);
    this.intervalMap[p.id] = setInterval(() => {
      this.nextImage(p.id, p.images.length);
    }, this.rotationDelay);
  }

  clearTimer(id: string) {
    const t = this.intervalMap[id];
    if (t) {
      clearInterval(t);
      delete this.intervalMap[id];
    }
  }

  stopAllTimers() {
    Object.keys(this.intervalMap).forEach(id => this.clearTimer(id));
  }

  getCurrentImage(project: Project) {
    if (!project.images || project.images.length === 0) return null;
    const idx = this.currentIndexMap[project.id] ?? 0;
    return project.images[idx] || project.images[0];
  }

  nextImage(id: string, len: number) {
    const cur = this.currentIndexMap[id] ?? 0;
    this.currentIndexMap[id] = (cur + 1) % len;
  }

  prevImage(id: string, len: number) {
    const cur = this.currentIndexMap[id] ?? 0;
    this.currentIndexMap[id] = (cur - 1 + len) % len;
  }

  // manual controls reset timer so user can interact without immediate auto-advance
  onNext(project: Project) {
    if (!project.images || project.images.length <= 1) return;
    this.nextImage(project.id, project.images.length);
    this.startTimerForProject(project);
  }

  onPrev(project: Project) {
    if (!project.images || project.images.length <= 1) return;
    this.prevImage(project.id, project.images.length);
    this.startTimerForProject(project);
  }

  // Touch handlers for swipe gestures
  onTouchStart(ev: TouchEvent, project: Project) {
    if (!project || !project.id) return;
    if (!ev.touches || ev.touches.length === 0) return;
    this.touchStartMap[project.id] = ev.touches[0].clientX;
    this.touchDeltaMap[project.id] = 0;
    // stop auto-rotation while interacting
    this.clearTimer(project.id);
  }

  onTouchMove(ev: TouchEvent, project: Project) {
    if (!project || !project.id) return;
    const start = this.touchStartMap[project.id];
    if (start === undefined) return;
    if (!ev.touches || ev.touches.length === 0) return;
    const current = ev.touches[0].clientX;
    this.touchDeltaMap[project.id] = current - start;
  }

  onTouchEnd(_ev: TouchEvent, project: Project) {
    if (!project || !project.id) return;
    const delta = this.touchDeltaMap[project.id] || 0;
    const threshold = 50; // px
    if (delta > threshold) {
      this.prevImage(project.id, project.images.length);
    } else if (delta < -threshold) {
      this.nextImage(project.id, project.images.length);
    }
    // cleanup
    delete this.touchStartMap[project.id];
    delete this.touchDeltaMap[project.id];
    // restart auto-rotation
    this.startTimerForProject(project);
  }
}
