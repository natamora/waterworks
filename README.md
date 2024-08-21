# Wodociągi

![Strona główna](https://github.com/natamora/natamora/blob/main/images/waterworks/mainPage.png)

## Opis projektu

Program powstał na potrzeby pracy inżynierskiej „GIS w wodociągach. Aplikacja do zarządzania wodomierzami”, zrealizowanej na Akademii Górniczo-Hutniczej w Krakowie pod opieką dr inż. Michała Lupy. 

Celem pracy było stworzenie aplikacji WebGIS dla przedsiębiorstw wodociągowych umożliwiającej zarządzanie bazą klientów oraz podgląd danych teleadresowych. 
Głównym elementem aplikacji jest podgląd lokalizacji obiektów na mapie oraz podgląd znajdujących się w nich wodomierzy.

## Technologie

* **ASP.NET Core 2.1**
* **OpenLayers, OSM**
* **PostgreSQL, PostGIS**
* **Bootstrap, JQuery**

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
## Przykładowe Rezultaty

* **Wyświetlanie oraz grupowanie obiektów na mapie**: Możliwość przeglądania budynków na mapie oraz tworzenia nowych obiektów poprzez dwukrotne kliknięcie. Możliwość podglądu podstawowych informacji o obiektach i zamontowania wodomierzy.

  ![Mapa obiektów](https://github.com/natamora/natamora/blob/main/images/waterworks/mapa.png)

* **Kartoteka wodomierzy**: Ekran kartoteki wodomierzy umożliwia przeglądanie wszystkich wodomierzy zarejestrowanych w systemie. Oferuje podobny widok dla kartotek kontrahentów oraz obiektów, co pozwala na spójne zarządzanie różnymi typami danych w aplikacji.

  ![Kartoteka wodomierzy](https://github.com/natamora/natamora/blob/main/images/waterworks/KartotekaWodomierzy.png)

* **Zarządzanie wodomierzami**: Możliwość zamontowania i wymiany wodomierzy w kartotece obiektu. 

  ![Wymiana wodomierza](https://github.com/natamora/natamora/blob/main/images/waterworks/WymienWodomierz2.png)

* **Podgląd informacji o klientach**: Wyświetlanie danych o klientach oraz związanych z nimi obiektach i umowach. Możliwość przeglądania i edytowania szczegółów.

  ![Kartoteka klienta](https://github.com/natamora/natamora/blob/main/images/waterworks/klient.png)

* **Zarządzanie umowami**: Dodawanie nowych umów oraz zarządzanie już istniejącymi.

  ![Zarządzanie umowami](https://github.com/natamora/natamora/blob/main/images/waterworks/umowy.png)

## 
All rights reserved
Copyright &copy; Katarzyna Morawska 2019
