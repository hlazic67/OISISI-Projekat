# Sajam knjiga

Projektni zadatak iz predmeta Osnovi informacionih sistema i softverskog inženjerstva (2026/2027).

## Student 1 - Helena Lazić
* **Entiteti:** Posetilac, Kupovina, Izdavac
* **DAO klase:** PosetilacDAO, KupovinaDAO, IzdavacDAO
* **Konzolni prikaz:** SajamConsoleView (prikaz, dodavanje, izmena i brisanje posetilaca, kupovina i izdavaca)
* **Deljeni deo:** Storage, Serializer, interfejs ISerijalizacija, Program.cs

## Student 2 - Emilija Milojević
* **Entiteti:** Adresa, Autor, Knjiga, kao i spojna klasa AutorKnjiga za vezu autor-knjiga
* **DAO klase:** AdresaDAO, AutorDAO, KnjigaDAO, AutorKnjigaDAO
* **Konzolni prikaz:** AutorKnjigaConsoleView (prikaz, dodavanje, izmena i brisanje autora i adresa, prikaz, dodavanje, izmena i brisanje knjiga) — povezivanje autora sa adresom i knjigama posle učitavanja iz fajlova
* **Deljeni deo:** Program.cs

## Struktura projekta

```text
sajam_knjiga/
├── Core/ zajednički sloj (modeli, pristup podacima)
│   ├── Models/ Adresa, Autor, Knjiga, AutorKnjiga, Posetilac, Kupovina, Izdavac
│   ├── DAO/ klase za pristup podacima (po jedna po entitetu)
│   ├── Interfaces/ ISerijalizacija
│   ├── Services/ Storage (čitanje i upis fajlova)
│   ├── Utils/ Serializer i pomoćne klase
│   └── Data/ CSV fajlovi sa podacima
└── Console/ konzolna aplikacija i meniji
