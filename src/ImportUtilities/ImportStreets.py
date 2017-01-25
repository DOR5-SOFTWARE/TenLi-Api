# -*- encoding: UTF-8 -*-

import codecs
import csv
import json
import re
import pymongo
from pymongo import MongoClient

mongoClient = MongoClient('localhost', 27017)
db = mongoClient.tenli
addressesCollection = db.Addresses

def insert_to_mongo(dto):

    return dto

    id = addressesCollection.insert_one(dto).inserted_id
    insertedDoc = addressesCollection.find_one({'_id': id})
    return insertedDoc

def import_streets():

    with open('Streets.csv', 'r') as csvFile:

        reader = csv.DictReader(csvFile, delimiter=',')

        for row in reader:
            try:
                addressDto = {
                    'City': row['city_name'].decode('utf8').strip(),
                    'Street': row['street_name'].decode('utf8').strip()
                }

                print(insert_to_mongo(addressDto))

            except Exception as e:
                print(e)
                continue

import_streets()
