# PL Generator importów do GnuCash 
Generator plików CSV z cenami i transakcjami dla GnuCash

## Spis treści

* [O projekcie](#o-projekcie)
  * [Generowanie pliku CSV z transakcjami](#generowanie-pliku-csv-z-transakcjami)
  * [Generowanie pliku CSV z cenami](#generowanie-pliku-csv-z-cenami)
* [Wymagania](#wymagania)
* [Konfiguracja](#konfiguracja)
* [Wykorzystywane technologie](#wykorzystywane-technologie)

## O projekcie
Projekt powstał, by ułatwić import danych o transakcjach z kont bankowych i danych o cenach papierów wartościowych do programu GnuCash.
Program generuje pliki CSV, które można łatwo zaimportować do programu GnuCash. Dostęp do bazy GnuCash odbywa się w trybie tylko do odczytu.

Obecnie projekt posiada 2 główne funkcjonalności: 
* Generowanie pliku CSV z transakcjami na podstawie plików generowanych przez systemy bankowości internetowej
* Generowanie pliku CSV z cenami papierów wartościowych

### Generowanie pliku CSV z transakcjami

Program pozwala na odczyt transakcji z kilku plików generowanych przez systemy bankowości internetowej i wygenerowanie z nich jednego pliku CSV zrozumiałego dla programu GnuCash.

Proces generowania pliku CSV wygląda następująco:
1. Użytkownik wskazuje pliki wygenerowane przez systemy bankowości internetowej do przetworzenia
2. Program pozwala sparować odpowiadające sobie transakcje z dwóch kont. Jeśli z konta A przelano 123.45 PLN na konto B program pozwoli zaimportować taką transakcję do GnuCash jako jedną transakcję pomiędzy kontem A i B.
Obecnie ten etap użytkownik musi wykonać ręcznie. Program pilnuje jednak, by kwoty sumowały się do zera. Na przykład nie można sparować wpływu 100.00 PLN i wypływu -50.00 PLN.
3. Przypisanie transakcji do kont wydatków i przychodów. Niesparowane transakcje program uznaje za przychody lub wydatki i za pomocą algorytmów machine learning stara się przypisać je do kont wydatków i przychodów.
4. Zaimportowane transakcje są wyświetlane użytkownikowi wraz z przypisanymi przez program kontami. Na tym etapie użytkownik może poprawić przypisanie transakcji do kont.
5. Zapis pliku CSV. W folderze aplikacji zapisywany jest plik transaction_import.csv

### Generowanie pliku CSV z cenami

Program pobiera dane z serwisu [info.bossa.pl](https://info.bossa.pl) i zapisuje do pliku CSV ceny dla zdefiniowanych w GnuCash papierów wartościowych.
Zapisywane są wyłącznie ceny z dat większych niż ostatnia data ceny papieru wartościowego w GnuCash

Obecnie pobierane są dane o następujących rodzajach aktywów:
* Kurs NBP walut
* Ceny funduszy inwestycyjnych

## Wymagania
Obecnie program współpracuje wyłącznie z danymi GnuCash zapisanymi jako baza SQLite. Jeśli wykorzystywany jest na przykład format zapisu danych GnuCash jako XML, program nie będzie działał.

## Konfiguracja

### Konfiguracja programu
Konfiguracja programu jest zapisana w pliku appsettings.json w folderze aplikacji.

W polu ConnectionStrings.GnuCashContext należy podać connection string do bazy SQLite programu GnuCash (odpowiednio sformatowana ścieżka do pliku).

### Konfiguracja GnuCash

Dla każdego konta bankowego dla którego będą do programu GnuCash importowane transakcje należy zdefiniować w programie GnuCash pole `Kod konta`.
Powinno ono odpowiadać numerowi konta w dokładnie takim formacie jak w pliku generowanym przez system bankowości internetowej.
Na przykład: W pliku XML wygenerowanym przez system bankowości internetowej banku PKO BP znajduje się linia `<account>21102010550000910201935659</account>`. 
Wartość pola `Kod konta` dla konta w programie GnuCash powinna być równa `21102010550000910201935659`

Pole `Rodzaj` dla zdefiniowanych w programie GnuCash funduszy inwestycyjnych powinno zawierać tylko ciąg `Fundusze inwestycyjne`, a pole `Symbol/skrót` powinno zawierać symbol 
zdefiniowany dla funduszu inwestycyjnego przez serwis [info.bossa.pl](https://info.bossa.pl)

## Wykorzystywane technologie
* .NET Core 3.1
* ML.NET
