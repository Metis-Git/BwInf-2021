#include <iostream>
#include <fstream>
#include <string>

#define maxDrivingTime 6*60
#define maxDays 5

struct Hotel {
    double rating;
    int time;
};

double bestPathRating = 0.0;
int bestPathTime = 10;
int* bestPath;

Hotel* hotels;
int endPointTime = 0;
int hotelCount = 0;

void loadFromFile(std::string path) {
    std::ifstream fileStream(path);
    std::string line;
    int lineCount = 0;

    while (getline(fileStream, line)) {
        if (lineCount == 0) {
            //inizializing the hotel-array
            hotelCount = stoi(line);
            hotels = new Hotel[hotelCount];
        }
        else if (lineCount == 1) {
            endPointTime = stoi(line);
        }
        else {
            Hotel temp;

            int index = line.find(" ");
            temp.time = stoi(line.substr(0, index));
            temp.rating = stod(line.substr(index+1, line.length()));

            hotels[lineCount - 2] = temp;
        }
        lineCount++;
    }

    fileStream.close();
}

void eval(int hotel, int prevs[], int level = 0) {
    // better path was already found
    if (hotels[hotel].rating <= bestPathRating) return;

    // adding the current hotel to the "list" of previous hotels
    prevs[level] = hotel;

    //evaluating the path
    if (endPointTime - hotels[hotel].time <= maxDrivingTime) {
        // searching the worst rating
        double minRating = 10.0;
        for (int i = 0; i < level+1; i++) {
            if (hotels[prevs[i]].rating < minRating) minRating = hotels[prevs[i]].rating;
        }

        // this path is better
        if (bestPathRating < minRating) {
            
            bestPathRating = minRating;
            bestPathTime = level;
            for (int i = 0; i <= level; i++) {
                bestPath[i] = prevs[i];
            }

            return;
        }
    }

    // Zeitüberschreitung
    if (level >= maxDays-1) return;


    // executing the next evaluation step
    for (int i = hotel + 1; i < hotelCount; i++) {
        if (hotels[i].time - hotels[hotel].time > maxDrivingTime) break;

        eval(i, prevs, level + 1);
    }
}



int main()
{
    // get file from user
    std::string path;
    std::cout << "Bitte geben Sie den Pfad zur Testdatei ein: ";
    std::cin >> path;

    // load configs from given file
    loadFromFile(path);
    std::cout << "Reisezeit: " << endPointTime << "min\nHotels: " << hotelCount << std::endl;
    std::cout << "==========\nStarting Search..." << std::endl;


    bestPath = new int[maxDays];

    int *heapArray = new int[5];
    for (int hotel = 0; hotel < hotelCount; hotel++) {
        if (hotels[hotel].time > maxDrivingTime) break;
        eval(hotel, heapArray);
    }
    
    if (bestPathTime > 5) std::cout << "Kein Pfad gefunden" << std::endl;
    else {
        std::cout << "Bester Pfad: ";
        for (int i = 0; i <= bestPathTime; i++) {
            std::cout << std::to_string(bestPath[i]);
            if (i != bestPathTime) std::cout << " --> ";
        }
            
        std::cout << " (schlechteste Bewertung: " << bestPathRating << ")" << std::endl;
    }
    
    delete[] heapArray;
    delete[] hotels;
    delete[] bestPath;
}

