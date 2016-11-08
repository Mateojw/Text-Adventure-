using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MyTextAdventure : MonoBehaviour {


    public int points;
    public AudioSource sfxSource;
	public Camera mainCam;
    
    //declaring an audioClip so I can change the SFX later.
    public AudioSource winSound;

	public Image portraitOfDiego;
	public Image keyImage;

	public string currentRoom;
	public string myText;
    private bool musicF = false;
	private bool Chalfpipe = false;
    private bool TFPoints = true;

    private bool Cjump = false;
    private bool Crail = false;
    private bool CRon = false;
    private bool CWill = false;

    //variables to store possible room connections.
    private string room_north;
	private string room_south;
	private string room_west;
	private string room_east;

	// Called the moment that the object is created
	// We use this for intitilization;
	void Start () {

        points = 0;
		//change text to read "We ran our scene."
		myText = "We ran our scene.";
		currentRoom = "title";
       
	}
	
	// Update is called once per frame
	void Update () {

       	
		//deactivate picture of diego if you're not in the title page or win room
		if (currentRoom == "title" || (currentRoom == "finish line" && !Chalfpipe))
		{
			portraitOfDiego.enabled = true;
		} else {
			portraitOfDiego.enabled = false;

			//if I wanted to change the image with another
			//portraitOfDiego.sprite = mySpriteVariable;
		}

		//only activate key image if you don't have the key, or you've used it to win
		if (currentRoom == "finish line" || !Chalfpipe)
		{
			keyImage.enabled = false ;
          

		} else {
			keyImage.enabled = true;
		}


		//we set our rooms to nil, so that if we haven't overwritten them by the time
		//we check for keypresses, we know there's no room.
		room_east = "nil";
		room_north = "nil";
		room_south = "nil";
		room_west = "nil";

		//resetting the background and text color, so that if i leave a room
		//where I change it, it doesn't stay that color
		mainCam.backgroundColor = Color.gray;
		GetComponent<Text>().color = Color.black;


        // if I'm in the entryway, I want the game to say "you are in the entryway."
        // else, check the other statements.
        if (currentRoom == "title")
        {
            myText = "radical ski Trip \n\nBy mateo\n\nPress Space to Begin";


            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentRoom = "lodge";
            }
        }
        else if (currentRoom == "lodge")
        {

            room_north = "Peak";

            myText = "you are in a ski lodge waiting to go skiing.\n";
            myText += "its warm and cozy but you are ready to shread some KNAR.";

            if (points >= 20)
            {
                myText = " Press B to buy some hot coco ";
                if (Input.GetKeyDown(KeyCode.B))
                {
                    currentRoom = "Coco";
                }
            }


        }
        else if (currentRoom == "Peak")
        {

            room_east = "park";
            room_south = "lodge";
            room_west = "DBD";

            myText = "you are on the Peak.";
            myText = "Ask skiing representatives for sponsorship \n press S to ask";
            if (Input.GetKeyDown(KeyCode.S))
            {

                currentRoom = "sponsor";

            }


            myText += " \n you have " + points + " points";
        }
        else if (currentRoom == "park")
        {

            room_west = "Peak";

            myText = "You are in the park.  you see sick rails, KNarly jumps, and a Tubular halfpipe";

            if (!Chalfpipe)
            {
                myText += "\n Press \"i\" to shread some Tubes.";

                if (Input.GetKeyDown(KeyCode.I))
                {

                    currentRoom = "halfpipe";

                }
            }

            if (!Cjump)
            {
                myText += "\n Press \"j\" to hit the jump.";

                if (Input.GetKeyDown(KeyCode.J))
                {

                    currentRoom = "jump";

                }
            }

            if (!Crail)
            {
                myText += "\n Press \"k\" to hit the rail.";

                if (Input.GetKeyDown(KeyCode.K))
                {

                    currentRoom = "rail";

                }
            }
        }

        else if (currentRoom == "DBD")
        {

            if (points <= 30)
            {
                myText = "You are not experienced enough to go down a double black diamond \n Press space to go back to the Peak";

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    currentRoom = "Peak";
                }
            }




            if (points > 30)
            {
                room_east = "Peak";

                myText = "You see the double black dimond is really steep do you ";

                if (!CRon)
                {
                    myText += "Press \"R\" to shread the mougals like Radio Ron. ";

                    if (Input.GetKeyDown(KeyCode.R))
                    {

                        currentRoom = "RadioR";

                    }
                }


                if (!CWill)
                {
                    myText += "Press \"W\" to shread the mougals like Will. ";

                    if (Input.GetKeyDown(KeyCode.W))
                    {

                        currentRoom = "Will";

                    }
                }


            }
        }





        else if (currentRoom == "halfpipe")
        {
            //changing background color and text color
            mainCam.backgroundColor = Color.gray;
            GetComponent<Text>().color = Color.black;

            myText = "You did a sick backflip! RAD! \n +10-20 Points \n Press spacebar to return to the park.";
            myText += " \n you got a rad picture to show to some sponsors, go find them and get that free swag";
            if (!Chalfpipe)
            {
                sfxSource.Play();
                var number = Random.Range(15, 20);
                points = points + number;
            }
            Chalfpipe = true;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentRoom = "park";
            }

        }



        else if (currentRoom == "sponsor")
        {
            //changing background color and text color
            mainCam.backgroundColor = Color.gray;
            GetComponent<Text>().color = Color.black;

            if (points < 60 || !Chalfpipe)
            {
                myText = "you ask for a sponsorship but you are not experienced enough come back with more points";
            }

            if (Chalfpipe == true && points < 60)
            {
                myText = "You may have as cool picture But thats not enough To get you a sponsorship keep ShReaDing that KNar mAn";

            }

            if (points > 60 && Chalfpipe == true)
            {
                myText = "You Got the sponsorship WOOOT FREEE SKIIIISSS";

                if (!musicF)
                {
                    winSound.Play();

                }
                musicF = true;
            }
            if (points > 73 && Chalfpipe == true)
            {
                myText = "you are a true master of your art please take my company and become the next great skiier of the world";

                if (!musicF)
                {
                    winSound.Play();

                }
                musicF = true;
            }


            myText += " \n Press spacebar to return to the Peak";

            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentRoom = "Peak";
            }

        }







        else if (currentRoom == "jump")
        {
            //changing background color and text color
            mainCam.backgroundColor = Color.gray;
            GetComponent<Text>().color = Color.black;

            myText = "You did a sick tweaked 1080! RAD! \n +10-20 Points \n Press spacebar to return to the park.";
            if (!Cjump)
            {
                sfxSource.Play();
                var number = Random.Range(15, 20);
                points = points + number;
            }
            Cjump = true;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentRoom = "park";
            }

        }

        else if (currentRoom == "RadioR")
        {
            //changing background color and text color
            mainCam.backgroundColor = Color.gray;
            GetComponent<Text>().color = Color.black;

            myText = "You shread the mougals screaming DA when you come in contact with the mougals,\n while this is loud and abnoxious ( attracting attention ) you go down the hill in record speed\n +10 for speed +0-10 for unique sounds";
            if (!CRon)
            {
                sfxSource.Play(); // sound of Radio Ron doing DA.
                var number = Random.Range(15, 20);
                points = points + number;
            }
            CRon = true;


            myText += "\n Press Space to go back to the Peak";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentRoom = "Peak";
            }

        }

        else if (currentRoom == "Will")
        {
            //changing background color and text color
            mainCam.backgroundColor = Color.gray;
            GetComponent<Text>().color = Color.black;

            myText = "You shread the mougals doing backflips and crazy nose butters... but whats that in the distance \n Radio Ron speeds by you screaming DA at every impact this distracts you causing you to fly into a tree and become paralized ";
            if (!CWill)
            {
                sfxSource.Play(); // sound of Radio Ron doing DA. 
            }
            myText += "\n Press Space to go back to the hospital";
            CWill = true;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentRoom = "Lose";
            }

        }

        else if (currentRoom == "rail")
        {
            //changing background color and text color
            mainCam.backgroundColor = Color.gray;
            GetComponent<Text>().color = Color.black;

            myText = "You did a kfed into a 450 out! Boomtastic! \n +10-15 Points \n Press spacebar to return to the park.";
            if (!Crail)
            {
                sfxSource.Play();
                var number = Random.Range(15, 20);
                points = points + number;
            }
            Crail = true;





            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentRoom = "park";
            }

        }




        else if (currentRoom == "Lose")
        {
            //changing background color and text color
            mainCam.backgroundColor = Color.gray;
            GetComponent<Text>().color = Color.black;

            myText = "you are injured and in the hospital :( for this year you cannot ski anymore \n press space to maracasly heal and start skiing again";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentRoom = "Peak";
            }

        }



        else if (currentRoom == "Coco")
        {
            //changing background color and text color
            mainCam.backgroundColor = Color.gray;
            GetComponent<Text>().color = Color.black;

            myText = "So tasty YUMMMMMM \n Press space to go back to the lodge";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentRoom = "lodge";
            }

        }





        else if (currentRoom == "finish line")
        {

            //sfxSource.clip = winSound;
            //if (!sfxSource.isPlaying) {
            //sfxSource.Play();
            //}

            if (Chalfpipe = true)
            {
                myText = "HEYOOOOO YOU GOT TO THE WIN ROOOM!!!! BOIIOIOIOIOING, NICE! Let'S ENJOY CAKE.";

            }
            else
            {

                myText = "Sorry, you unlucky person, you need a key.\n\n Press space to return to the Peak";

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    currentRoom = "Peak";
                }

            }


        }
        else
        {

            myText = "You have fallen into a void because the game designer is a garbage game designer and the developer is bad too and some variable is set wrong, specifically currentRoom.";

        }


		// here we're checking for keyboard input
		// if a directional key is pressed
		// we go to the corresponding room.

		myText += "\n\n";
		if (room_north != "nil"){

			myText += "Press Up to go to the " + room_north + "\n";

			if (Input.GetKeyDown(KeyCode.UpArrow)) {
				
				currentRoom = room_north;

			}
		}


		if (room_south != "nil"){

			myText += "Press Down to go to the " + room_south + "\n";

			if (Input.GetKeyDown(KeyCode.DownArrow)){
				
				
				currentRoom = room_south;

			}
		}
	
		if (room_east != "nil"){

			myText += "Press Right to go to the " + room_east + "\n";

			if (Input.GetKeyDown(KeyCode.RightArrow)){
				
				currentRoom = room_east;

			}
		}

		if (room_west != "nil") {

			myText += "Press Left to go to the " + room_west + "\n";

			if (Input.GetKeyDown(KeyCode.LeftArrow)){
				
				currentRoom = room_west;

			}
		}

		//We are acccesing the text component, then using dot notation
		//to modify the text attribute.
		GetComponent<Text>().text = myText;

	}

}
