# Waterworks

![Homepage](https://github.com/natamora/natamora/blob/main/images/waterworks/mainPage.png)

## Project Description

This program was developed for the engineering thesis *“GIS in waterworks. Application for water metres management.”*, completed at AGH University of Science and Technology in Kraków under the supervision of Dr Eng. Michał Lupa.

The goal of the project was to develop a WebGIS application for water utility companies that allows managing a customer database and viewing contact information. The core functionality includes displaying facility locations on a map and showing the water meters associated with those facilities.

## Technologies

* **ASP.NET Core 2.1**  
* **OpenLayers, OpenStreetMap (OSM)**  
* **PostgreSQL, PostGIS**  
* **Bootstrap, jQuery**

## Application Features

* Displaying buildings and basic information about them on the map  
* Searching coordinates based on addresses  
* Creating new objects by double-clicking on the map area  
* Adding new clients, viewing their contact details, and associated objects  
* Displaying water meters registered in the system, with options to install, detach, or replace watermeters in buildings  
* Viewing contract information and adding new contracts  

## Database

An important part of the project was designing the database schema:

![Database schema designed for the project](https://github.com/natamora/natamora/blob/main/images/waterworks/baza.png)

## Sample Results

* **Displaying and grouping objects on the map**: Enables browsing buildings on the map and creating new objects by double-clicking. Provides basic information preview about objects and the ability to install water meters.

  ![Map of objects](https://github.com/natamora/natamora/blob/main/images/waterworks/mapa.png)

* **Water meter registry**: The water meter registry screen allows viewing all meters registered in the system. Similar views are available for client and object registries, enabling consistent data management across different entities.

  ![Water meter registry](https://github.com/natamora/natamora/blob/main/images/waterworks/KartotekaWodomierzy.png)

* **Water meter management**: Ability to install and replace water meters within the object registry.

  ![Water meter replacement](https://github.com/natamora/natamora/blob/main/images/waterworks/WymienWodomierz2.png)

* **Client information overview**: Displays client data along with related objects and contracts, with options to view and edit details.

  ![Client registry](https://github.com/natamora/natamora/blob/main/images/waterworks/klient.png)

* **Contract management**: Adding new contracts and managing existing ones.

  ![Contract management](https://github.com/natamora/natamora/blob/main/images/waterworks/umowy.png)

##

All rights reserved  
Copyright &copy; Katarzyna Morawska 2019
