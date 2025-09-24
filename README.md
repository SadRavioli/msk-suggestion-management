# MSK Suggestion Management

A full-stack web application for managing employee office health and safety suggestions, built with Vue.js frontend and .NET Core backend.

## Running the Application

### Prerequisites

**Node.js 20.19.0+ or 22.12.0+**

**.NET 8 SDK**

### Setup & Start

1. **Start the Backend API**
   ```bash
   cd src/MSKSuggestionManagement.API
   dotnet restore
   dotnet run
   ```
   Swagger UI available at `http://localhost:5246/swagger/` or `https://localhost:7241/swagger` in development

2. **Start the Frontend** (separate terminal)
   ```bash
   cd frontend
   npm install
   npm run dev
   ```
   Frontend will be available at `http://localhost:5173`

The frontend will automatically proxy API requests to the backend.

## Assumptions Made

1. **Entity Relationship**: One-to-many relationship between employee and suggestion
2. **Data Volume**: Moderate scale (hundreds of suggestions, not thousands)
3. **Tables/Cards**: Clear rows of data, management page for mobile and desktop

## Architectural Decisions

### Backend (.NET Core)
Clean Architecture implementation with clear separation of concerns:

```
src/
├── MSKSuggestionManagement.API/            # Web API controllers, Swagger
├── MSKSuggestionManagement.Application/    # Business logic, DTOs, services
├── MSKSuggestionManagement.Domain/         # Entities, enums, interfaces
└── MSKSuggestionManagement.Infrastructure/ # Data access, EF Core, repositories
```

**Key choices:**
- **Clean Architecture**: Enables testability, maintainability, and clear separation of concerns, SOLID
- **Repository Pattern**: Data access abstraction for easier testing and future database changes
- **Entity Framework Core**: Code-First approach with proper migrations
- **RESTful API**: Standard HTTP verbs and status codes for predictable API behavior

### Frontend (Vue.js)
Modern Vue 3 application structured for developer productivity:

```
frontend/src/
├── components/             # Reusable UI components
├── services/               # API communication layer
└── main.js                 # Application entry point
```

**Key choices:**
- **Vue 3 Options API**: First time using, liked the template structure
- **Bootstrap 5**: Rapid development with proven accessibility and responsive features, use bootstrap 4 a lot at current job
- **Mobile-First Design**: Cards for touch interfaces (< 1200px), table for desktop information density
- **Accessibility Focus**: WCAG 2.1 compliance with ARIA labels and keyboard navigation

### Responsive Strategy
- **Mobile/Tablet**: Card layout optimized for touch interaction and limited screen space
- **Desktop**: Table layout for information density and data scanning
- **Breakpoint**: 1200px chosen specifically to accommodate 11-column table without horizontal scroll
