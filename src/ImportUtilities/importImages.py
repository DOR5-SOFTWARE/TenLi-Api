import csv, json, random, re, requests, pymongo, shutil, os

gender = "Female"

from pymongo import MongoClient
mongoClient = MongoClient('localhost', 27017)
db = mongoClient.tenli
imagesCollection = db.Images

def insert_to_mongo(dto):
	id = imagesCollection.replace_one(dto, dto, True).upserted_id
	insertedDoc = imagesCollection.find_one({'_id' : id})
	print('inserted to mongo')
	return insertedDoc

def get_images_urls(username):
	print('retrieving images urls')

	content_header = {'Content-Type': 'application/json'}
	r = requests.get(
		"http://uifaces.com/api/v1/user/" + username,
		data=None, headers=content_header)
	r.raise_for_status()
	return r.text

def download_image(image_url, username, size):
	print('starting download : ' + image_url)

	r = requests.get(image_url, stream=True)
	r.raise_for_status()
	file_path = "Images/RandomUserImages/" + username + "/" + size + ".jpg"
	os.makedirs(os.path.dirname(file_path), exist_ok=True)
	with open(file_path, 'wb') as f:
		r.raw.decode_content = True
		shutil.copyfileobj(r.raw, f)
	
	print('created file:' + file_path)

def import_images_from_uifaces():
	with open('FemaleUsernames.csv', newline='', encoding='Latin-1') as csvFile:

		reader = csv.DictReader(csvFile, delimiter='\t')

		for row in reader:
			try:
				username = row['Username']
				response = json.loads(get_images_urls(username))				
								
				imageDto = {
					'Small' : response['image_urls']['mini'],
					'Medium' : response['image_urls']['bigger'],
					'Large' : response['image_urls']['epic'],
				}

				for size in imageDto:
					download_image(imageDto[size], username, size)
				
				localImage = {
					'Small' : "/Images/RandomUserImages/" + username + "/Small.jpg",
					'Medium' : "/Images/RandomUserImages/" + username + "/Medium.jpg",
					'Large' : "/Images/RandomUserImages/" + username + "/Large.jpg",
					'Gender' : gender
				}

				insert_to_mongo(localImage)
				
				print("download images for "+ username +" complete")

			except Exception as e:
				print(e)
				continue

import_images_from_uifaces()
