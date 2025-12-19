# Localization System Documentation

## Overview

This is a database-driven localization system for the ASP.NET Core Web API project. It supports key-based localization where the UI always works with keys (e.g., `DashboardPage.Welcome`), and all translations are stored in the database.

## Supported Languages

- **Azerbaijani (az)**
- **English (en)** - Default fallback language
- **Russian (ru)**

## Database Setup

1. Run the SQL script `LocalizationSchema.sql` to create the necessary tables:
   ```sql
   -- The script creates:
   -- - Languages table
   -- - LocalizationResources table
   -- - Indexes for performance
   -- - Default language entries (az, en, ru)
   ```

2. The tables will be created in your SQLite database (EfendiDemo.db by default).

## API Endpoints

### Get Translation by Key and Language
```
GET /api/localization?lang=az&key=DashboardPage.Welcome
```
**Response:** `"Xoş gəlmişsiniz!"`

### Get All Translations for a Language
```
GET /api/localization/all?lang=az
```
**Response:** 
```json
{
  "DashboardPage.Welcome": "Xoş gəlmişsiniz!",
  "DashboardPage.Title": "Ana Səhifə"
}
```

### Add Single Translation
```
POST /api/localization
Content-Type: application/json

{
  "key": "DashboardPage.Welcome",
  "value": "Xoş gəlmişsiniz!",
  "languageCode": "az"
}
```

### Add Multiple Translations (Bulk)
```
POST /api/localization/bulk
Content-Type: application/json

[
  {
    "key": "DashboardPage.Welcome",
    "value": "Xoş gəlmişsiniz!",
    "languageCode": "az"
  },
  {
    "key": "DashboardPage.Welcome",
    "value": "Welcome!",
    "languageCode": "en"
  },
  {
    "key": "DashboardPage.Welcome",
    "value": "Добро пожаловать!",
    "languageCode": "ru"
  }
]
```

### Update Translation
```
PUT /api/localization
Content-Type: application/json

{
  "key": "DashboardPage.Welcome",
  "value": "Updated translation",
  "languageCode": "az"
}
```

### Delete Translation
```
DELETE /api/localization?id=1
```

### Get All Languages
```
GET /api/localization/languages
```

### Get All Resources (Admin)
```
GET /api/localization/resources
```

## Fallback Logic

The system implements intelligent fallback:

1. **Requested Language**: First tries to get the translation in the requested language
2. **English Fallback**: If not found and the requested language is not English, falls back to English
3. **Key Return**: If still not found, returns the key itself

**Example:**
- Request: `GET /api/localization?lang=az&key=DashboardPage.Welcome`
- If Azerbaijani translation exists → Returns Azerbaijani text
- If Azerbaijani translation missing but English exists → Returns English text
- If both missing → Returns `"DashboardPage.Welcome"`

## Usage Example

### Client-Side Integration

```javascript
// Fetch translation
async function getTranslation(key, lang = 'en') {
  const response = await fetch(`/api/localization?lang=${lang}&key=${key}`);
  return await response.text();
}

// Usage
const welcomeText = await getTranslation('DashboardPage.Welcome', 'az');
// Returns: "Xoş gəlmişsiniz!"
```

### Adding Translations

```javascript
// Add a new translation
async function addTranslation(key, value, languageCode) {
  await fetch('/api/localization', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ key, value, languageCode })
  });
}

// Add multiple translations at once
async function addBulkTranslations(translations) {
  await fetch('/api/localization/bulk', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(translations)
  });
}
```

## Architecture

The system follows clean architecture principles:

- **Models**: `Core/Models/Language.cs`, `Core/Models/LocalizationResource.cs`
- **Repository Layer**: CQRS pattern with Commands and Queries
- **Service Layer**: Business logic with fallback handling
- **Controller Layer**: RESTful API endpoints

## Best Practices

1. **Key Naming**: Use hierarchical keys like `PageName.SectionName.KeyName`
   - Example: `DashboardPage.Welcome`, `ProductPage.Title`

2. **Language Codes**: Always use lowercase language codes (az, en, ru)

3. **Missing Translations**: The system gracefully handles missing translations by falling back to English or returning the key

4. **Performance**: Indexes are created on frequently queried columns (Key, LanguageCode)

5. **Scalability**: Easy to add new languages by inserting into the Languages table and adding translations

## Adding a New Language

1. Insert into Languages table:
   ```sql
   INSERT INTO Languages (Code, Name, IsActive) 
   VALUES ('fr', 'French', 1);
   ```

2. Add translations via API or directly in the database

3. The system will automatically support the new language

