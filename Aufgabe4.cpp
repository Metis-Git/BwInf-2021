#include <iostream>
#include <fstream>
#include <random>
#include <string>

std::string info = "BwInf 2021 - Aufgabe 4: \"W\x81rfelgl\x81ck\"\nTeam-ID: 00564, Team-Name: \"SRZ info3 Gruppe 1\"\n";


void readFile()
{
	std::string in = "";
	
	while (in == "")
	{
		std::cout << "Bitte geben Sie den Pfad zu der Datei ein: ";
		std::getline(std::cin, in);
	}
	
	uint8_t** dices;
	uint8_t* dices_counts;
	
	std::ifstream file = std::ifstream();
	file.open(in);
	
	if (file.is_open()) {
		
		std::getline(file, in);
		
		uint8_t dices_count;
		uint8_t index = 0;
		dices_count = std::stoi(in);
		dices_counts = new uint8_t[dices_count];
		dices = new uint8_t*[dices_count];
		
		while (std::getline(file, in)) {
			
		}
	}
	
}

int main()
{
	std::cout << info << std::endl;
	
	readFile();
	
	while (true)
	{

	invalid_answer:
	
		std::string answer = "";
	
		std::cout << "Noch eine Datei lesen? [J/N] ";
		std::getline(std::cin, answer);
		
		if (!(answer == "J" || answer == "j"))
			if (answer == "N" || answer == "n") break;
			else goto invalid_answer;
			
		system("cls");
		
		std::cout << info << std::endl;
		
		readFile();
	}

	return 0;
}