import csv, json, re, pymongo

from pymongo import MongoClient
mongoClient = MongoClient('localhost', 27017)
db = mongoClient.tenli
firstnamesCollection = db.firstnames

def insert_to_mongo(dto):
	id = firstnamesCollection.insert_one(dto).inserted_id
	insertedDoc = firstnamesCollection.find_one({'_id' : id})
	return insertedDoc

def transmit_csv_data_to_robomow_id():

	with open('Firstnames4Tenli.csv', newline='', encoding='Utf-8') as csvFile:

		reader = csv.DictReader(csvFile, delimiter='\t')

		for row in reader:
			try:
				firstnameDto = {
					'EngValue' : row['EngValue'],
					'HebValue' : row['HebValue'],
					'Gender' : row['Gender']
				}

				print(insert_to_mongo(firstnameDto))
				
			except Exception as e:
				print(e)
				continue

transmit_csv_data_to_robomow_id()
