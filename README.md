# Sajam knjiga

Projektni zadatak iz predmeta Osnovi informacionih sistema i softverskog inženjerstva (2026/2027).

## Student 1 - Helena Lazić
* **Entiteti:** Posetilac, Kupovina, Izdavac
* **DAO klase:** PosetilacDAO, KupovinaDAO, IzdavacDAO
* **Servisi:** PosetilacService, KupovinaService, IzdavacService
* **Konzolni prikaz:** PosetilacConsoleView, (prikaz, dodavanje, izmena i brisanje posetilaca)
  KupovinaConsoleView (prikaz, dodavanje, izmena i brisanje kupovina) i IzdavaciConsoleView (prikaz, dodavanje, izmena i brisanje izdavaca)
* **Deljeni deo:** Storage, Serializer, interfejs ISerijalizacija, Program.cs

## Student 2 - Emilija Milojević
* **Entiteti:** Adresa, Autor, Knjiga, kao i spojna klasa AutorKnjiga za vezu autor-knjiga
* **DAO klase:** AdresaDAO, AutorDAO, KnjigaDAO, AutorKnjigaDAO
* **Servisi:** AdresaService, AutorService, KnjigaService(povezivanje autora sa adresom i knjigama posle učitavanja iz fajlova, provere i brisanje uz vođenje računa o referencijalnim zavisnostima)
* **Konzolni prikaz:** AutorConsoleView(prikaz, dodavanje, izmena i brisanje autora), KnjigaConsoleView(prikaz, dodavanje, izmena i brisanje knjiga) i AdresaConsoleView(prikaz, dodavanje, izmena i brisanje adresa)
* **Deljeni deo:** Program.cs
```
## Struktura projekta

sajam_knjiga/
│
├── Core/
│   │
│   ├── Models/
│   │   ├── Adresa.cs
│   │   ├── Autor.cs
│   │   ├── Knjiga.cs
│   │   ├── AutorKnjiga.cs
│   │   ├── Posetilac.cs
│   │   ├── Kupovina.cs
│   │   └── Izdavac.cs
│   │
│   ├── DAO/
│   │   ├── AdresaDAO.cs
│   │   ├── AutorDAO.cs
│   │   ├── KnjigaDAO.cs
│   │   ├── AutorKnjigaDAO.cs
│   │   ├── PosetilacDAO.cs
│   │   ├── KupovinaDAO.cs
│   │   └── IzdavacDAO.cs
│   │
│   ├── Interfaces/
│   │   └── ISerijalizacija.cs
│   │
│   ├── Services/
│   │   ├── Storage.cs
|   |   ├── AdresaService.cs
│   │   ├── AutorService.cs
│   │   ├── KnjigaService.cs
│   │   ├── PosetilacService.cs
│   │   ├── KupovinaService.cs
│   │   └── IzdavacService.cs

│   │
│   ├── Utils/ 
|   |   Serializer.cs 
│   │   
│   └── Data/
│       ├── adrese.csv
│       ├── autori.csv
│       ├── knjige.csv
│       ├── autor_knjiga.csv
│       ├── posetioci.csv
│       ├── kupovine.csv
│       └── izdavaci.csv
│
└── Console/
    │
    ├── Program.cs
    │
    └── ConsoleUI/
        ├── PosetilacConsoleView.cs
        ├── KupovinaConsoleView.cs
        ├── IzdavaciConsoleView.cs
        ├── AutorConsoleView.cs
        ├── KnjigaConsoleView.cs
        ├── AdresaConsoleView.cs
        └── ConsoleViewUtils.cs
