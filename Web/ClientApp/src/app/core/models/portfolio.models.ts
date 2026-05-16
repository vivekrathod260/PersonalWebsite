export interface Profile {
  name: string;
  title: string;
  intro: string;
  about: string;
  imageUrl: string;
  resumeUrl: string;
  email: string;
  location: string;
  ctaPrimary: string;
  ctaSecondary: string;
}

export interface Project {
  id: string;
  title: string;
  description: string;
  techStack: string[];
  images: string[];
  videoUrl: string;
  githubUrl: string;
  liveUrl: string;
  tags: string[];
  featured: boolean;
}

export interface Experience {
  id: string;
  company: string;
  role: string;
  description: string;
  startDate: string;
  endDate: string;
  current: boolean;
  experienceSkills?: ExperienceSkill[];
}

export interface ExperienceSkill {
  name: string;
  iconUrl?: string;
  highlight?: boolean;
}
export interface SkillCategory {
  category: string;
  skills: Skill[];
}

export interface Skill {
  id: string;
  name: string;
  category: string;
  iconUrl: string;
  proficiency: number;
}

export interface Testimonial {
  id: string;
  name: string;
  company: string;
  designation: string;
  review: string;
  imageUrl: string;
}

export interface Social {
  id: string;
  platform: string;
  url: string;
  icon: string;
}

export interface ContactForm {
  name: string;
  email: string;
  subject: string;
  message: string;
}

export interface Settings {
  showHero?: boolean;
  showAbout?: boolean;
  showExperience?: boolean;
  showProjects?: boolean;
  showSkills?: boolean;
  showTestimonials?: boolean;
  showContact?: boolean;
  showFooter?: boolean;
  showNavbar?: boolean;
}
