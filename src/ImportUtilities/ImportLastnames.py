import csv, json, re, pymongo

from pymongo import MongoClient
mongoClient = MongoClient('localhost', 27017)
db = mongoClient.tenli
lastnamesCollection = db.lastnames

def insert_to_mongo(dto):
	id = lastnamesCollection.insert_one(dto).inserted_id
	insertedDoc = lastnamesCollection.find_one({'_id' : id})
	return insertedDoc

def import_lastnames():

	with open('Lastnames4Tenli.csv', newline='', encoding='Utf-8') as csvFile:

		reader = csv.DictReader(csvFile, delimiter='\t')

		for row in reader:
			try:
				lastnameDto = {
					'EngValue' : row['EngValue'],
					'HebValue' : row['HebValue']
				}

				print(insert_to_mongo(lastnameDto))
				
			except Exception as e:
				print(e)
				continue

import_lastnames()
