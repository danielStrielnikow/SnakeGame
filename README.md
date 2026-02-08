# SnakeGame

Prosta gra Snake napisana w C# jako projekt konsolowy. Projekt realizowany w ramach zajec z programowania w zespole dwuosobowym z wykorzystaniem GitHub.

## Opis gry

Gracz steruje wezem, ktory porusza sie po planszy. Celem jest zjadanie jedzenia i zdobywanie punktow. Waz rosnie po kazdym zjedzeniu. Gra konczy sie gdy waz uderzy w sciane lub w samego siebie. Co 3 punkty gra przyspiesza i rosnie poziom trudnosci.

## Sterowanie

- Strzalka w gore - ruch do gory
- Strzalka w dol - ruch w dol
- Strzalka w lewo - ruch w lewo
- Strzalka w prawo - ruch w prawo

Waz nie moze zawrocic w przeciwnym kierunku.

## Jak uruchomic

1. Zainstaluj .NET SDK (wersja 6.0 lub nowsza)
2. Sklonuj repozytorium:
```
git clone https://github.com/danielStrielnikow/SnakeGame.git
```
3. Przejdz do folderu projektu:
```
cd SnakeGame
```
4. Uruchom gre:
```
dotnet run
```

## Struktura projektu

- `Snake.cs` - glowna logika gry (ruch, sterowanie, kolizje, punktacja)
- `Pixel.cs` - klasa reprezentujaca pojedynczy piksel na ekranie
- `Obstakel.cs` - klasa przeszkody
- `SnakeGame.csproj` - plik projektu C#

## Zespol

- Daniel Strielnikow - ruch weza, obsługa klawiszy, kolizje, poziom trudnosci
- Drugi czlonek zespolu - jedzenie, punktacja, dokumentacja

## Zasady wspolpracy

1. Kazdy czlonek zespolu pracuje na swojej galezi (branch)
2. Po zakonczeniu zadania tworzymy Pull Request do galezi main
3. Drugi czlonek zespolu robi code review
4. Po zaakceptowaniu zmian mergujemy do main
5. W razie konfliktow rozwiazujemy je przed mergem
