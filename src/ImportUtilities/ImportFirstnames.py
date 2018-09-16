# -*- encoding: UTF-8 -*-

import codecs
import csv
import json
import re
import pymongo

from pymongo import MongoClient
mongoClient = MongoClient('localhost', 27017)
db = mongoClient.tenli
firstnamesCollection = db.Firstnames


def insert_to_mongo(dto):

    # return dto

    id = firstnamesCollection.insert_one(dto).inserted_id
    insertedDoc = firstnamesCollection.find_one({'_id': id})
    return insertedDoc


def import_firstnames():

    with open('Firstnames4Tenli.csv', 'r') as csvFile:

        reader = csv.DictReader(csvFile, delimiter='\t')

        for row in reader:
            try:
                firstnameDto = {
                    'EngValue': row['EngValue'].strip(),
                    'HebValue': row['HebValue'].decode('utf8').strip(),
                    'Gender': row['Gender'].strip()
                }

                print(insert_to_mongo(firstnameDto))

            except Exception as e:
                print(e)
                continue


import_firstnames()
