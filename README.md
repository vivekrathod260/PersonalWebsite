# Portfolio Website

A modern, data-driven portfolio website built with ASP.NET Core Web API and Angular, powered by Firebase Firestore.

## Architecture

```
??? Web/          ? ASP.NET Core API + Angular SPA host
??? Business/     ? Service layer, DTOs, business logic
??? Data/         ? Firebase Firestore repository, models, configuration
??? Web/ClientApp ? Angular 17 frontend (standalone components, Tailwind CSS)
```

## Prerequisites

- .NET 8 SDK
- Node.js 18+
- Firebase project with Firestore enabled

## Setup

### 1. Firebase Configuration

1. Create a Firebase project at [console.firebase.google.com](https://console.firebase.google.com)
2. Enable Cloud Firestore
3. Generate a service account key (Project Settings ? Service Accounts ? Generate New Private Key)
4. Save the JSON file as `firebase-credentials.json` in the `Web/` directory
5. Update `Web/appsettings.json`:

```json
{
  "Firebase": {
    "ProjectId": "your-firebase-project-id",
    "CredentialPath": "firebase-credentials.json"
  }
}
```

### 2. Firebase Collections

Create these Firestore collections:

- **profile** (single document with id `main`): name, title, intro, about, imageUrl, resumeUrl, email, location, ctaPrimary, ctaSecondary
- **projects**: title, description, techStack[], images[], videoUrl, githubUrl, liveUrl, tags[], featured, order
- **experiences**: company, role, description, startDate, endDate, current, order
- **skills**: name, category, iconUrl, proficiency, order
- **testimonials**: name, company, designation, review, imageUrl, order
- **socials**: platform, url, icon, order
- **contact**: (auto-created by form submissions)

### 3. Backend

```bash
cd Web
dotnet run
```

API available at `https://localhost:7000/swagger`

### 4. Frontend

```bash
cd Web/ClientApp
npm install
npm start
```

Angular dev server at `http://localhost:4466`

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/portfolio/profile | Get profile data |
| GET | /api/portfolio/projects | Get all projects |
| GET | /api/portfolio/projects/featured | Get featured projects |
| GET | /api/portfolio/experiences | Get work experiences |
| GET | /api/portfolio/skills | Get skills grouped by category |
| GET | /api/portfolio/testimonials | Get testimonials |
| GET | /api/portfolio/socials | Get social links |
| POST | /api/portfolio/contact | Submit contact form |

## Production Build

```bash
cd Web/ClientApp
npm run build
cd ..
dotnet publish -c Release
```

The Angular build output is served as static files by the ASP.NET Core app.
