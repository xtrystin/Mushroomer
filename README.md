# Mushroomer
The goal of this project is to create software for a mushroom pickers' community portal.
Both functional and non-functional requirements were implemented, which were identified
during an analysis of the project's subject. As part of the work, an analysis of available
technologies was performed. Based on the results, the most suitable architecture and
technology stack were chosen.\
The system was deployed to the Azure cloud environment, making the services publicly
available. This setup ensures easy transformation of the system into a production-ready
state. At the end a summary of the project was presented and potential areas for future
development were identified.

### Bachelor’s thesis - Mushroom Pickers community portal
[bachelorThesis.pdf](https://github.com/xtrystin/Mushroomer/files/14130140/bachelorThesis.pdf)

### Live Preview
<https://xtrystin.github.io/Mushroomer/>\
_Services are hosted on shared servers. First app load every few hours can take up to 1 minute_

### Presentation Videos
#### Mushrooms Locations on Map
[![Mushrooms Locations on Map](https://img.youtube.com/vi/x9MD21PKAns/0.jpg)](https://www.youtube.com/watch?v=x9MD21PKAns)
#### Posts
[![Posts](https://img.youtube.com/vi/qebYCD2M-Y4/0.jpg)](https://www.youtube.com/watch?v=qebYCD2M-Y4)
#### Weatherforecast
[![Posts](https://img.youtube.com/vi/UX2zFZoGkSE/0.jpg)](https://www.youtube.com/watch?v=UX2zFZoGkSE)


### Główne funkcje produktu
1. Lista grzybów i informacje o nich
    - Dodanie nowego grzyba
    - Informacja czy grzyb jest jadalny
    - Wyświetlanie listy grzybów
    - Sortowanie grzybów
    - Zmodyfikowanie grzyba
    - Wyświetlenie szczegółów o grzybie
2. Posty
    - Tworzenie
    - Ocenianie
    - Wyświetlanie
    - Podział na kategorie, np. Jak zbierać, przepisy, hodowla, poradniki, zdjęcia
    - Moderacja przez admina
3. Profil użytkownika
    - Wyświetlanie postów i komentarzy danego użytkownika
    - Wyświetlanie profilu
    - Opis profilu
    - Wyszukanie profilu
    - Poziom doświadczenia
4. Lista znajomych
    - Dodawanie/Usuwanie ze znajomych
    - Wyświetlanie znajomych
    - Wyszukiwanie znajomych po imieniu, nazwisku, e-mail
5. Konto użytkownika
    - Tworzenie konta
    - Logowanie
    - Zmiana hasła
6. Ostrzeżenia
    - Oznaczanie miejsc trujących grzybów
    - Ocenianie przez społeczność czy wyznaczone miejsce jest prawdziwe, poprawne
    - Wymaganie 3 pozytywnych ocen od doświadczonych, grzybiarzy żeby ostrzeżenie pokazywało się poczatkującym

### Wymagania funkcjonalne
1.	Tworzenie i logowanie się do kont
2.	Profil użytkownika
3.	Ranga profilu: początkujący, doświadczony
4.	Lista znajomych
5.	Informowanie o grzybach: trujące, nietrujące
6.	Wyznaczanie miejsc gdzie są grzyby trujące
7.	Chat
8.	Przeglądanie postów innych użytkowników
9.	Informacje/ostrzeżenia o grzybach
10.	Zaznaczanie lokacji grzybów w postach na mapie
11.	Tworzenie postów
12.	Typy postów: tekstowe, zdjęcia, poradniki
13.	Przeglądanie, edycja własnych postów
14.	System polubienia postów
15.	Moderacja postów
16.	Filtrowanie i sortowanie postów
17.	Oferty kupna/sprzedaży

### Wymagania niefunkcjonalne
1.	Dostępność systemu na systemach Linux, Windows, Mac, IOS, Android
2.	Niezawodność - strona może być niedostępna max 10 dni w roku
3.	Użytkowanie - przejrzyste, łatwe w obsłudze UI
4.	Dostępność dla niedowidzących - odpowiedni rozmiar czcionki na stronie
5.	Przechowywanie min dwóch kopii zapasowych danych
6.	Maintainability - w razie awarii system powinien być odtworzony w max 1 dzień
7.	Szybkość działania - strona powinna się załadować w max 2s
8.	Wydajność - obsługa min. 1000 requestów/sec
9.	Pokrycie ważnych funkcji testami jednostkowymi
10.	Bezpieczeństwo - przeprowadzanie audytu bezpieczeństwa min raz w roku
11.	Kod ma być pisany zachowując wzorce clean code, SOLID, KISS, itp.. Kod ma być samodokumentujący się (użycie odpowiednich nazw zmiennych, funkcji)


### System Architecture
![image](https://github.com/xtrystin/Mushroomer/assets/33805319/6ff8ffab-20ee-4e98-91db-07da8f4a2412)

Backend składa się z 4 mikroserwisów związanych z domeną oprogramowania oraz serwis autoryzacyjny. Microserwisy utworzone są w architekturze Clean Architecture – podział na warstwę domeny, aplikacji, infrastructure, API. W celu kontroli wersji i historii kodu został wykorzystany system GIT. Repozytorium jest hostowane na serwisie Github. Komunikacja między mikroserwisami odbywa się asynchronicznie z wykorzystaniem Azure Service Bus. Każdy message jest zapisywany do bazy MessageDb, dzięki temu mamy historię wszystkich wiadomości, która może pomóc przy analizie, debugowaniu.

Każdy mikroserwis odpowiada danemu wycinkowi domeny oprogramowania (bounded context): 
- Post Microservice odpowiada za tworzenie, modyfikowanie, usuwanie, pobieranie, like/dislike postów i komentarzy
- User Microservice – dodawanie informacji o nowym użytkowniku, pobieranie informacji  o wszystkich użytkownikach lub o konkretnym użytkowniku na podstawie userId
- Warning Microservice – zarządzanie ostrzeżeniami, dodanie approve/disapprove do ostrzeżenia
- Mushroom Microservice – proste operacje CRUD na grzybach – odczyt wszystkich, pojedynczego, modyfikacja, usunięcie, dodanie.
- AuthService – tworzenie konta użytkownika, zapis do bazy zahashowanego hasła, walidacja lognu i hasła oraz generowanie tokenu JWT, 
Autoryzacja użytkownika w każdym mikroserwisie jest na podstawie tokenu JWT, który jest weryfikowany w każdym mikroserwisie. Token jest generowany przez AuthService. Aplikacja frontowa wysyła  HTTP POST z nazwą i hasłem użytkownika, w odpowiedzi dostaje token zawierający claims takie jak: nazwa użytkownika, userId, role użytkownika, czas wygaśnięcia tokenu. 
Przykładowy zwracany token JWT:

![image](https://github.com/xtrystin/Mushroomer/assets/33805319/4d089aaf-d51e-4e32-b5c9-20a7ef229be5)

**API Gateway** – pośredniczy w komunikacji między aplikacją frontową a mikroserwisami. Pełni funkcję Reverse Proxy - do niego trafiają requesty, które są przekierowywane do odpowiednich serwisów. Aplikacja kliencka nie mu wiedzieć do jakiego miekroserwisu trafi request – dzięki temu pisanie frontu jest bardziej efektywne, po każdej zmienie w backendzie nie trzeba zmieniać kodu na frontcie tylko wystarczy (jeśli trzeba) w Api Gateway. 
Kolejną zaletą jest to, że po stronie backendu łatwo podmienić mikroserwis na inny, np. w przypadku dużego ruchu część requestów można przekierować do innej instancji tego samego mikroserwisu (load balancer).

**WebAssemblyUI** – aplikacja frontowa znajdująca się całkowicie po stronie klienta napisana przy użyciu technologii Blazor WebAssembly .NET 6.0. Zawiera logikę odpowiadającą za pobranie daynch od użytkownika, przesłanie ich na backend przez API Gateway oraz wyświetleniu danych pobranych z API na stronie internetowej. 
Posiada bibliotekę, która zawiera funkcje do komunikacji z API za pomocą HttpClient.

**AuthDb** – baza danych MS SQL zawierająca dane wrażliwe użytkownika – nazwa użytkownika, hasło, role (admin, moderator, zwykły użytkownik), Wykorzystywana jest jedynie przez AuthService, który wykonuje czynności Authenticate -  dodaje nowego użytkownika, przypisuje do roli, sprawdza poprawność hasła

**ResoureDb** – baza danych MS SQL zawierająca dane związane z domeną oprogramowania – między innymi posty, grzyby, warningi. Wykorzystywana jest przez mikroserwisy: Post, Warning, Mushroom, User, które łączą się z nią za pomocą ConnectionString przechowywanej jako zmienna konfigurowalna w appsettings.json

Message Logger Service – mikroserwis, który subskrybuje wszystkie kolejki, odbiera od nich asynchronicznie wiadomości i zapisuje do bazy MessageDb

MessageDb – baza danych MS SQL zawierająca wszystkie wiadomości, które znajdowały się na wszystkich kolejkach w Azure Service Bus. Wiadomości mogą zostać wykorzystane w analizie ruchu, debugowaniu błędów 

### Architektura komunikacji asynchronicznej między mikroserwisami
![image](https://github.com/xtrystin/Mushroomer/assets/33805319/ab2aeb5d-5ef7-4312-aa96-3b8fad8e36c6)

#### Wykorzystanie asynchronicznej komunikacji 
Do architektura można dodać komunikację asynchroniczną między mikroserwisami przy wykorzystaniu kolejki, np. Azure Service Bus. Zapewni to niezależność mikroserwisów (decoupled), szybszą komunikację, skalowalność, łatwą rozbudowę - można dodać nowego subscribera niezmieniając kodu publishera.
W komunikacji asynchronicznej występują również wyzwania, np. brak gwarancji odebrania wiadomości. W związku z tym należy zadbać o odpowiednią telemetrię, logowanie oraz monitorownie wiadomości wysłanych oraz odebranie w celu wykrywania oraz debuggowania błędów. Do tych celów można wykorzystać usługę Azure Application Insights, które udostępnia bibliotekę do .NET 6.0. 
Oprócz tego można użyć bazę danych do której trafiałyby wszystkie wiadomości, dzięki temu można logować wszystkie eventy trafiające na kolejki oraz analizować je w momencie wystąpienia błędu.
Potrzebny byłby do tego osobny mikroserwis MessageLoggerService, który subskrybowałby wszystkie kolejki, a otrzymane od nich wiadomości zapisywał do osobnej bazy danych, po to by nie generować dodatkowego ruchu w bazie przechowującej dane aplikacji (grzyby, warnings, post)

W implementacji należy zdefiniować publisher’ów oraz subscriber’ów, strukturę wiadomości, urn wiadomości, format wiadomości (xml, json, txt) oraz topic/kolejkę na którą ma trafiać wiadomość lub być z niej odczytywana. 
Dodatkowo należy rozważyć czy wiadomość na mieć wielu consumerów – użycie Topic, czy jednego – należy użyć Queue.
Do wysyłania oraz asynchronicznego odbioru wiadomości można użyć darmowej oraz open-sourced biblioteki MassTransit w wersji 8, która działa w środowisku .NET 6.0.

Przykładowe wiadomości, które można użyć w systemie:
Queue UserCreatedQueue
Publisher: AuthService
Subscriber: UserService
Zastosowanie: Dane wrażliwe klienta (login, hasło)v zapisywane są przez AuthService w osobnej bazie AuthDb. Jeśli chcemy mieć dane klienta(imie, nazwisko, id) w bazie z danymi ResourceDb należy przesłać je, żeby mógł je odebrać UserService, który wpisze je do bazy ResourceDb.
Przykładowa wiadomość w postaci JSON:
```json
UserCreatedMessage{
    "urn": "AuthService.UserCreatedMessage",
    "message":{
        "UserId": "aaa-bfds-43535345-234a",
        "Firstname": "John",
        "Lastname": "Smith",
        "EmailAddress": "jsmith@mail.com"
    }
}
```
Jeśli pozostałe mikroserwisy potrzebowałyby użyć tej wiadomości, to można je  w prosty sposób oraz niskim kosztem dodać jako subscriberów.
Żeby rozbudować komunikację asynchroniczną o inne wiadomości należałoby dokonać głębszej analizy – jaką część komunikacji synchronicznej można przerobić na asynchroniczną. Wymaga to dodatkowego kosztu czasu.
Jeśli w przyszłej rozbudowanie powstałaby potrzebna komunikacji między mikroserwisami to mając już gotową infrastrukturę w postaci jednej kolejki w Azure Service Bus można w prosty sposób dodać kolejną kolejkę umożliwiając wprowadzenie komunikacji asynchronicznej niskim kosztem wdrożenia.
Asynchroniczną komuniakcję między mikroserwisami można rozbudować o publikowanie i subskrybowanie kolejnych wiadomości: MushroomAddedQueue, MushroomDeletedQueue, CommentAddedQueue, CommentLikedMessage.

### Opis architektury pojedynczego mikroserwisu na podstawie Post Microservice

Została wykorzystana architektura Clean Architecture

![image](https://github.com/xtrystin/Mushroomer/assets/33805319/6f2ec85a-c283-4b06-a1d0-fdeba626fb15)

Implementacja w kodzie – struktura folderów i projektów

![image](https://github.com/xtrystin/Mushroomer/assets/33805319/a16c74b0-613b-4035-a375-63b34de17fe0)
![image](https://github.com/xtrystin/Mushroomer/assets/33805319/1811d680-1417-4d30-870d-5fec4ed1c60d)

1. Post.API – ASP.NET web API MVC. Zawiera Controllers, które posiadają funkcje do obsługi requstów HTTP – GET, POST, PATCH, PUT, DELETE. Zaimplementowane jest Dependency Injection oraz mediator. który rozsyła wiadomości do odpowiednich handlerów w warstwie aplikacji.

![image](https://github.com/xtrystin/Mushroomer/assets/33805319/b99111e4-e2ec-4584-b061-aaf4c466c716)

2. Post.Application – class library .net 6.0. Jest to warstwa aplikacji implementująca wzorzec CQRS. Zawiera osobno comendy (create, update, delete) oraz quey(read). W tej warstwie zawarte są również commandHanlders, które zawierają obsługę danej komendy.

![image](https://github.com/xtrystin/Mushroomer/assets/33805319/0d92f446-1a58-430c-b49e-3056dcf18d0d)
![image](https://github.com/xtrystin/Mushroomer/assets/33805319/1744fd22-fb6c-446d-bc10-44c526456a2c)

3. Post.Domain – class library .net 6.0. Warstwa domeny zawierająca Entity modelujące dany wycinek domeny (bounded context) Postów. Dodatkowo posiada ValueObjects z klasami reprezentującymi dane wartości, np. PostId posiadający wartość Guid -> umożliwiają one uniknięcia Primitive Obsession. Posiada również Repository z interface IPostRepository, który zawiera deklaracje metod do pobierania, zapisywania, zmieniania, usuwania obiektu Post w bazie danych. Implementacja tego interface znajduje się w warstwie Infrastructure.

![image](https://github.com/xtrystin/Mushroomer/assets/33805319/08c37faa-624c-4f8e-a70e-5ff14872c29e)
![image](https://github.com/xtrystin/Mushroomer/assets/33805319/f2b7a290-c790-494d-aa50-aad67ac70a04)

4. Post.Infrastructure – class library .net 6.0 z Entity Framework 6.0. Odpowiada za połączenie z bazą danych oraz zapisywanie i pobieranie obiektów. Posiada również QueryHandlers, które obsługują Query z CQRS za pomocą osobnej klasy do odczytu bazy danych PostReadDbContext, która umożliwia wyłącznie odczyt bez możliwości zapisu -> rozdział operacji modyfikującej encje Commands od Queries. 
Zawiera konfigurację do mapowia klas na tabele w bazie danych. Operacje wykonywane na bazie danych są za pomocą odpowiedniego DbContexu oraz LINQ.
Przykład – wycinek z implemetacji interface IPostRepository

![image](https://github.com/xtrystin/Mushroomer/assets/33805319/b30f86d4-a9a1-40bb-9908-28d587cb8731)
![image](https://github.com/xtrystin/Mushroomer/assets/33805319/2498ace9-d8dc-412c-9908-d304db6a9920)

