# -*- encoding: UTF-8 -*-

import codecs
import csv
import json
import re
import pymongo

from pymongo import MongoClient
mongoClient = MongoClient('localhost', 27017)
db = mongoClient.tenli
lastnamesCollection = db.Lastnames


def insert_to_mongo(dto):

    return dto

    id = lastnamesCollection.insert_one(dto).inserted_id
    insertedDoc = lastnamesCollection.find_one({'_id': id})
    return insertedDoc


def import_lastnames():

    with open('Lastnames4Tenli.csv', 'r') as csvFile:

        reader = csv.DictReader(csvFile, delimiter='\t')

        for row in reader:
            try:
                lastnameDto = {
                    'EngValue': row['EngValue'].strip(),
                    'HebValue': row['HebValue'].decode('utf8').strip()
                }

                print(insert_to_mongo(lastnameDto))

            except Exception as e:
                print(e)
                continue

import_lastnames()
