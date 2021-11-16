// JA-1.cpp
//

#include <iostream>
#include <iomanip>
#include <fstream>
#include <string>
#include <windows.h>

#define CHRONOMETER

#ifdef CHRONOMETER
#include <chrono>
std::chrono::high_resolution_clock::time_point start_point;
#endif // CHRONOMETER

int16_t x_diff, y_diff, x, y;
float tmp;

// bad bit magic
__forceinline int16_t xValue(uint32_t* coordinate) {
	return (int16_t)(*coordinate >> 16);
}

__forceinline int16_t yValue(uint32_t* coordinate) {
	return (int16_t)(*coordinate & 0x0000FFFF);
}

__forceinline uint32_t store(int32_t x, int32_t y) {
	return (uint32_t)x << 16 | ((uint32_t)y & 0x0000FFFF);
}

__forceinline void findNearest(uint16_t windTurbine_index, uint32_t* arr, size_t* maxNum_houses, size_t* house_index, float* distance) {

	x_diff = -xValue(&arr[windTurbine_index]),
	y_diff = -yValue(&arr[windTurbine_index]),
	x = xValue(&arr[0]) + x_diff,
	y = yValue(&arr[0]) + y_diff;

	*house_index = 0;
	*distance = std::sqrtf((float)(x * x + y * y));

	for (uint16_t t = 1; t < *maxNum_houses; t++) {
		x = xValue(&arr[t]) + x_diff,
		y = yValue(&arr[t]) + y_diff;

		tmp = std::sqrtf((float)(x * x + y * y));
		
		if (*distance > tmp)
		{
			*house_index = t;
			*distance = tmp;
		}
	}
}

void readFile() {

	std::string ret = "";

	// +-- X ---------------+-- Y ---------------+
	// |0000 0000  0000 0000|0000 0000  0000 0000|
	uint32_t* coordinates;
	size_t house_index;
	float distance;

	while (ret == "")
	{
		std::cout << "Bitte geben Sie den Pfad zu der Datei ein: ";
		std::getline(std::cin, ret);
	}

#ifdef CHRONOMETER
	start_point = std::chrono::high_resolution_clock::now();
#endif

	std::ifstream file = std::ifstream();
	file.open(ret);

	if (file.is_open()) {

		size_t houses, windTurbines;

		{	// Einlesen
			std::getline(file, ret);
			size_t index = ret.find(' ');
			size_t counter = 0;

			houses = std::stoi(ret.substr(0, index));
			windTurbines = std::stoi(ret.substr(index, ret.length()));

			coordinates = new uint32_t[houses + windTurbines];

			while (std::getline(file, ret))
			{
				index = ret.find(' ');
				coordinates[counter] = store(std::stoi(ret.substr(0, index)), std::stoi(ret.substr(index, ret.length())));
				counter++;
			}
		}

		file.close();

		{
			HANDLE current = GetStdHandle(STD_OUTPUT_HANDLE);

			std::cout
				<< "Windr\x84 \bder: " << windTurbines << "; H\x84 \buser: " << houses
				<< "\n\n\xBA";

			SetConsoleTextAttribute(current, 0x0007 | COMMON_LVB_GRID_HORIZONTAL);

			std::cout << std::setw(27) << "Windrad" << std::setw(20) << "\xBA" << std::setw(17) << "Haus" << std::setw(13) << " ";
			
			SetConsoleTextAttribute(current, 0x0007);

			std::cout << "\xBA\n\xBA";

			SetConsoleTextAttribute(current, 0x0007 | COMMON_LVB_UNDERSCORE | COMMON_LVB_GRID_HORIZONTAL);

			std::cout
				<< "  Index \xB3" << std::setw(16) << " an Position" << std::setw(6) << "\xB3" << " maximale H\x94he"
				<< " \xBA  Index \xB3" << std::setw(16) << " an Position" << "     ";

			SetConsoleTextAttribute(current, 0x0007);
			std::cout << "\xBA" << std::endl;
		}

		for (uint16_t t = 0; t < windTurbines; t++) {

			findNearest(t + houses, coordinates, &houses, &house_index, &distance);

			// Ausgabe
			std::cout
				<< "\xBA " << std::setw(6) << t + 1 << " \xB3 ( "
				<< std::setw(6) << xValue(&coordinates[t + houses]) << " | " << std::setw(6) << yValue(&coordinates[t + houses]) << " ) \xB3"
				<< std::setw(12) << std::fixed << std::setprecision(2) << distance / 10 << " m \xBA "
				<< std::setw(6) << 1 + house_index << " \xB3 ( "
				<< std::setw(6) << xValue(&coordinates[house_index]) << " | " << std::setw(6) << yValue(&coordinates[house_index]) << " ) \xBA" << std::endl;
		}

	}
	else {
		std::cerr << "Datei konnte nicht ge\x94 \bffnet werden!" << std::endl;
		return;
	}

#ifdef CHRONOMETER
	std::chrono::high_resolution_clock::duration time = std::chrono::high_resolution_clock::now() - start_point;

	uint16_t
		min = std::chrono::duration_cast<std::chrono::minutes>(time).count(),
		sec = std::chrono::duration_cast<std::chrono::seconds>(time -= std::chrono::minutes(min)).count(),
		mili_sec = std::chrono::duration_cast<std::chrono::milliseconds>(time -= std::chrono::seconds(sec)).count(),
		micro_sec = std::chrono::duration_cast<std::chrono::microseconds>(time -= std::chrono::milliseconds(mili_sec)).count();

	std::cout << "\nben\224tigte Berechnungszeit: " << (min != 0 ? std::to_string(min) + " min, " : "");
	std::cout << (sec != 0 ? std::to_string(sec) + " s, " : "") << mili_sec << " ms, " << micro_sec << " \xE6s" << std::endl;
#endif
}


__forceinline void header() {
	HANDLE current = GetStdHandle(STD_OUTPUT_HANDLE);
	SetConsoleTextAttribute(current, 0x000F | COMMON_LVB_UNDERSCORE);
	std::cout << "BwInf 2021 - Junioraufgabe 1: \"Zum Winde verweht\"";
	SetConsoleTextAttribute(current, 0x0000);
	std::cout << "\xDB" << std::endl;
	SetConsoleTextAttribute(current, 0x0007);
	std::cout << "\nTeam-ID: ";
	SetConsoleTextAttribute(current, FOREGROUND_GREEN | FOREGROUND_RED | FOREGROUND_INTENSITY);
	std::cout << "00564";
	SetConsoleTextAttribute(current, 0x0007);
	std::cout << ", Team-Name: \"";
	SetConsoleTextAttribute(current, FOREGROUND_GREEN | FOREGROUND_RED | FOREGROUND_INTENSITY);
	std::cout << "SRZ info3 Gruppe 1";
	SetConsoleTextAttribute(current, 0x0007);
	std::cout << "\"\n" << std::endl;
	readFile();
}

int main()
{

	system("title BwInf 2021 - Junioraufgabe 1: \"Zum Winde verweht\"");
	
	HANDLE current = GetStdHandle(STD_OUTPUT_HANDLE);
	header();

	while (true)
	{
		std::string answer = "";

	invalid_answer:
		std::cout << "Noch eine Datei lesen? [J/N] ";
		std::getline(std::cin, answer);
		if (!(answer == "J" || answer == "j"))
			if (answer == "N" || answer == "n") break;
			else goto invalid_answer;
		system("cls");
		header();
	}

	return 0;
}

