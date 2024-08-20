![Strona główna](https://github.com/natamora/natamora/blob/main/images/waterworks/mainPage.png)
## Wodociągi
Program powstał na potrzeby pracy inżynierskiej „GIS w wodociągach. Aplikacja do zarządzania wodomierzami”, zrealizowanej na Akademii Górniczo-Hutniczej w Krakowie pod opieką dr inż. Michała Lupy.

Celem pracy było stworzenie aplikacji WebGIS dla przedsiębiorstw wodociągowych umożliwiający zarządzanie bazą klientów oraz podgląd danych teleadresowych.
Głównym elementem aplikacji jest podgląd lokalizacji obiektów na mapie oraz podgląd znajdujących się w nich wodomierzy.

## Technologie
* ASP.NET Core 2.1
* OpenLayers, OSM
* PostgreSQL, PostGIS
* Bootstrap, JQuery

## Założenia aplikacji
* Wyświetlanie budynków oraz podstawowych informacji o nich na mapie
* Wyszukiwanie współrzędnych na podstawie adresu
* Tworzenie nowych obiektów po dwukrotnym kliknięciu na obszar mapy
* Tworzenie nowych kontrahentów, podgląd danych teleadresowych oraz obiektów do nich należących
* Wyświetlanie wodomierzy znajdujących się w systemie. Możliwość zamontowania, odpięcia lub wymiany wodomierza w budynku
* Podgląd informacji o zawartych umowach oraz dodawanie nowych
  
## Baza danych
Ważnym elementem pracy było zaprojektowanie bazy danych:

![Baza danych zaprojektowana na potrzeby projektu](https://github.com/natamora/natamora/blob/main/images/waterworks/baza.png)

## Rezultaty
Wyświetlanie oraz grupowanie budynków na mapie. Po dwukrotnym kliknięciu na obszar mapy możliwość stworzenia nowego obiektu. Po kliknięciu na obiekt możliwość podglądu podstawowych informacji oraz zamontowania wodomierza.

![Mapa obiektów](https://github.com/natamora/natamora/blob/main/images/waterworks/mapa.png)

Podgląd wszystkich kartotek wodomierzy, kontrahentów, obiektów itp.

![Kartoteka wodomierzy](https://github.com/natamora/natamora/blob/main/images/waterworks/KartotekaWodomierzy.png)

Możliwość zamontowania i wymiany wodomierzy w kartotece obiektu.

![Kartoteka wodomierzy](https://github.com/natamora/natamora/blob/main/images/waterworks/WymienWodomierz2.png)

Możliwość podglądu informacji o kliencie, a także o związanych z nim obiektach i umowach.

![Kartoteka wodomierzy](https://github.com/natamora/natamora/blob/main/images/waterworks/klient.png)

Dodawanie nowych typów umów.

![Kartoteka wodomierzy](https://github.com/natamora/natamora/blob/main/images/waterworks/umowy.png)






## 
All rights reserved
Copyright &copy; Katarzyna Morawska 2019
