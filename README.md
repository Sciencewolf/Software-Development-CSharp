# Alapkövetelmények

Bármely alapkövetelmény hiánya esetén a feladat értékelhetetlennek
minősül! (Elégtelen érdemjegy)

- A projektnek van saját Git repository-ja GitHub-on
– A fejlesztés során rendszeres commit-ok történtek
- Egy solution használata
- A solution-ben minden projekt legalább .NET 8-es verziót használ
- Legalább két külön projekt megléte a solution-ben a feladat követelményei alapján (ha szükséges lehet több is):
– ASP.NET Core Web API projekt
– Blazor WebAssembly projekt
- A solution build-elhető

# Feladat leírás

A feladat egy könyvtárban működő webes alkalmazás készítése, amely a könyvek
és azok kikölcsönzésének nyilvántartására szolgál.

# Követelmények

- Az egyes könyvek kezeléséhez legyen leimplementálva minden CRUD művelet, valamint legyen hozzájuk a frontenden lista nézet
- Az egyes olvasók kezeléséhez legyen implementálva minden CRUD művelet, valamint lehessen a frontenden kilistázni az olvasókat
- A könyveket lehessen kikölcsönözni, lehessen kilistázni azokat valamint az egyes emberek által kikölcsönzött könyveket

# Kezelendő adatok

## Könyvek

- Leltári szám (EF által automatikusan generált)
- Könyv címe
- Könyv szerzője
- Kiadó
- Kiadás éve
- Validációs kikötés: Az érték nem lehet negatív

## Olvasók

- Olvasószám (EF által automatikusan generált)
- Olvasó neve
- Lakcím
- Születési dátum
- Validációs kikötés: Az érték nem lehet kisebb mint 1900

## Kölcsönzés

- Olvasószám
- Könyv leltári száma
- Kölcsönzés ideje
- Validációs kikötés: Nem lehet a jelenlegi napnál korábbi
- Visszahozási határidő
- Validációs kikötés: A visszahozás ideje később kell legyen, mint a kikölcsönzés ideje

# Technikai követelmények

- UNIT tesztek a WebApi service-ekre
- Adatbázis kezelés EF használatával (Használt DB maga szabadon választható)
- Model validáció
- Front- és Backend oldalon egyaránt!
- Minden mezőre érvényes validáció: az adott mezők nem lehetnek üresek valamint nem tartalmazhatnak csak whitespace karaktereket
- A külön mező specifikus validációk a Kezelendő adatok szekció alatt találhatóak meg