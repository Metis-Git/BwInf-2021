#include <iostream>
#include <fstream>
#include <string>
#include <chrono>
#include <conio.h>

#define CHRONOMETER
#define maxDrivingTime 6*60
#define maxDays 5

struct Hotel {
	uint8_t rating;
	int16_t time;
} __packed;

int* bestPath;
uint8_t bestPathRating;
int bestPathTime;

Hotel* hotels;
int endPointTime;
int hotelCount;

const char* header = "BwInf 2021 - Aufgabe 2 \"Vollgeladen\"\n\0";

#ifdef CHRONOMETER
std::chrono::high_resolution_clock::time_point start_point;
unsigned long calltime;
#endif

void eval(int hotel, int prevs[], int level = 0) {

	calltime++;

	// better path was already found
	if (hotels[hotel].rating <= bestPathRating) return;

	// adding the current hotel to the "list" of previous hotels
	prevs[level] = hotel;

	//evaluating the path
	if (endPointTime - hotels[hotel].time <= maxDrivingTime) {
		// searching the worst rating
		uint8_t minRating = 100;
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

		eval(i, prevs, level+1);
	}
}

void evaluateFile() {

	// measurement of time
#ifdef CHRONOMETER
	calltime = 0;
	start_point = std::chrono::high_resolution_clock::now();
#endif

	//// Loading data from file ////
	hotelCount = 0;
	endPointTime = 0;

	// get file from user
	std::string path;
	std::cout << "Bitte geben Sie den Pfad zur Testdatei ein: ";
	std::getline(std::cin, path);

	// load configs from given file
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
			if (hotelCount < (lineCount - 2)) break;
			Hotel temp;

			int index = line.find(" ");
			temp.time = stoi(line.substr(0, index));
			temp.rating = (uint8_t)(stod(line.substr(index + 1, line.length())) * 10);

			hotels[lineCount - 2] = temp;
		}
		lineCount++;
	}

	fileStream.close();
	////        ////


	if (hotelCount <= 0) {
		std::cout << "keine Hotels geladen!\n" << std::endl;
		return;
	}

	std::cout << "Reisezeit: " << endPointTime << " min\nHotels: " << hotelCount << std::endl;
	std::cout << "==========\nStarting Search..." << std::endl;

	//
	bestPath = new int[maxDays];
	bestPathRating = 0;
	bestPathTime = 10;

	// start evaluation for the hotels reachable on the first day
	int* heapArray = new int[5];
	for (int hotel = 0; hotel < hotelCount; hotel++) {
		if (hotels[hotel].time > maxDrivingTime) break;
		eval(hotel, heapArray);
	}

	// printing the result
	if (bestPathTime > 5) std::cout << "Kein Pfad gefunden" << std::endl;
	else {
		std::cout << "Bester Pfad: ";
		for (int i = 0; i <= bestPathTime; i++) {
			std::cout << std::to_string(bestPath[i]);
			if (i != bestPathTime) std::cout << " --> ";
		}

		std::cout << " (schlechteste Bewertung: " << ((double)bestPathRating) / 10.0 << ")" << std::endl;
	}

#ifdef CHRONOMETER
	std::chrono::high_resolution_clock::duration time = std::chrono::high_resolution_clock::now() - start_point;

	uint16_t min = std::chrono::duration_cast<std::chrono::minutes>(time).count();
	uint16_t sec = std::chrono::duration_cast<std::chrono::seconds>(time -= std::chrono::minutes(min)).count();
	uint16_t mili_sec = std::chrono::duration_cast<std::chrono::milliseconds>(time -= std::chrono::seconds(sec)).count();
	uint16_t micro_sec = std::chrono::duration_cast<std::chrono::microseconds>(time -= std::chrono::milliseconds(mili_sec)).count();

	std::cout << "ben\224tigte Berechnungszeit: " << (min != 0 ? std::to_string(min) + " min, " : "");
	std::cout << (sec != 0 ? std::to_string(sec) + " s, " : "") << mili_sec << " ms, " << micro_sec << " \xE6s" << std::endl;
	std::cout << "Anzahl an Aufrufen: " << calltime << std::endl;
#endif

	delete[] heapArray;
	delete[] hotels;
	delete[] bestPath;
}

int main()
{
	std::cout << header;

	evaluateFile();

	std::string answer;

	while (true) {

		std::cout << "noch eine Datei? [y/n] ";
		std::getline(std::cin, answer);

		if (answer != "y") break;

		evaluateFile();
	}
}

