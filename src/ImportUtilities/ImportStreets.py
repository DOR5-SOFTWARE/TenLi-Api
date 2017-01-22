import docstring
import csv
import json
import re
import pymongo

from pymongo import MongoClient

mongoClient = MongoClient('localhost', 27017)
db = mongoClient.tenli
streetsCollection = db.streets


def insert_to_mongo(dto):
    id = streetsCollection.insert_one(dto).inserted_id
    insertedDoc = streetsCollection.find_one({'_id': id})
    return insertedDoc


def import_streets():

    with open('Streets.csv', encoding='Utf-8') as csvFile:

        reader = csv.DictReader(csvFile, delimiter=',')

        for row in reader:
            try:
                streetDto = {
                    'StreetName': row['street_name'],
                    'CityName': row['city_name']
                }
                print (streetDto)
                #print(insert_to_mongo(firstnameDto))

            except Exception as e:
                print(e)
                continue

import_streets()
