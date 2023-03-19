using UnityEngine;
using System.Collections;

// [RequireComponent (typeof (Detonator))]
// [AddComponentMenu("Detonator/Force")]
public class DetonatorForce : MonoBehaviour 
{
	public bool BOMBNUKE;
	public bool NUKE;
	public bool NUKEhuge;
	public bool TOXIC;

	public bool lkt;
	public bool lSkt;
	public bool lMt;
	public bool S0Mt;

	public bool PlaySoundOnce;
	public bool PlaySoundOnceW;
	public bool PlaySoundOnceW1;
	public bool PSonce1;
	public bool PSonce2;
	public bool PSonce3;
	private float _baseRadius = 50.0f;
	private float _basePower = 4000.0f;
	private float _scaledRange;
	private float _scaledIntensity;
	private bool _delayedExplosionStarted = false;
	private float _explodeDelay;
	
	public float radius;
	public float power;
	public GameObject fireObject;
	public float fireObjectLife;
	
	public Collider[] _collidersVeryFar;   /////  VeryFar Kaking NPC  !!!!!!!!!!!!!!!!   Change Layer to  ONLY AFFECT RIDGED BODY!!
	// public Collider[] _colliders;  		/////   FAR AWAY OBJECTS
	// public Collider[] _collidersMedium;    /////   MEDIUM OBJECTS FROM EXPLOSION
	// public Collider[] _collidersNearFireZone;  ///// FireZone
	// public Collider[] _collidersNear;		///// 	CLOSE OBJECTS (TOTAL DEATH ZONE)
	// public Collider[] _collidersNearSUB1;		///// 	CLOSE OBJECTS (TOTAL DEATH ZONE SUB)
	// public Collider[] _collidersNearDELETE;    ///////////   DELETE GAME OBJECTS   
	public Collider[] _collidersFARpanic;    ///////////   DELETE GAME OBJECTS   
	private GameObject _tempFireObject;

	public GameObject LevelControl;

	public bool OnceOnly;
	public bool OnceOnly2;

	public GameObject Ragdoll;

	public GameObject NukeRepExpSmall;
	public GameObject NukeRepBloodSplat;
	

	public bool NPConFire;
	public float size = 10;
	public float ForceX2 = 1;
	
	public LayerMask Maskedlayers;
	public LayerMask MaskedlayersForce;
	public LayerMask MaskedlayersForce2;
	public LayerMask MaskedlayersNPC;
	//public LayerMask MaskedlayersTREENPC;

	public bool EFFECTS = false;
	private float Ammount;
	

	//public var  TS;

	// override public void Init()
	// {
	// 	//unused
	// }
	void Awake (){

		if(PlayerPrefs.GetString("ALLEFFECTS") == "EFFECTSON"){
			EFFECTS = true;
		}
		 Maskedlayers =  (1 << LayerMask.NameToLayer("Default")) 
					| (1 << LayerMask.NameToLayer("TREE"));
		 MaskedlayersForce =  (1 << LayerMask.NameToLayer("ADDforce")) 
                    | (1 << LayerMask.NameToLayer("TREE"));
		 MaskedlayersNPC = LayerMask.GetMask("NPC");
		 
		 MaskedlayersForce2 = (1 << LayerMask.NameToLayer("ADFHum"));
		// MaskedlayersTREENPC =  (1 << LayerMask.NameToLayer("TREE")) 
        //            | (1 << LayerMask.NameToLayer("NPC"));		 //LayerMask.GetMask("Default");
		 PlaySoundOnce = true;
		 PlaySoundOnceW = true;
		 PlaySoundOnceW1 = true;
		 PSonce1 = true;
		 PSonce2 = true;
		 PSonce3 = true;
		 OnceOnly = true;
		 OnceOnly2 = true;
		 LevelControl = GameObject.Find("LevelControl");
		 if(BOMBNUKE){
			NPConFire = false;
			if(EFFECTS){
		 StartCoroutine(WAS());}
		 	if(!EFFECTS){
				if(!NUKE){
		 StartCoroutine(WAS());}}
		//  if(NUKE){
		// 	if(!NUKEhuge){
		// 		StartCoroutine(ST());
		// 	}
		//  }
		 }
		 	

	}
	

	void Start()
	{
		// LevelControl.GetComponent<LevelPointControl>().Casulties += Random.Range(0,10);
		// LevelControl.GetComponent<LevelPointControl>().CasultiescounterCIV += Random.Range(0,10);
		// LevelControl.GetComponent<LevelPointControl>().CasultiescounterMIL += Random.Range(0,10);
		// LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(0,10);
		// LevelControl.GetComponent<LevelPointControl>().SPECIALcount += Random.Range(0,10);


		// if (_delayedExplosionStarted)
		// {
		// 	_explodeDelay = (_explodeDelay - Time.deltaTime);
		// 	if (_explodeDelay <= 0f)
		// 	{
				if(OnceOnly){
					power = Random.Range((power * 1.4f),(power * 1.7f));
					if(NUKE){
						ForceX2 = 4.1f;
					}
				Explode();
			OnceOnly = false;
			}}
		//}
	//}
	
	private Vector3 _explosionPosition;
	
	public void Explode()
	{	

		// if (!on) return;
		// if (detailThreshold > detail) return;	
		
		// if (!_delayedExplosionStarted)
		// {
		// 	_explodeDelay = explodeDelayMin + (Random.value * (explodeDelayMax - explodeDelayMin));
		// }
		// if (_explodeDelay <= 0) //if the delayTime is zero
		// {
			//tweak the position such that the explosion center is related to the explosion's direction
			_explosionPosition = transform.position; //- Vector3.Normalize(MyDetonator().direction);
			//_colliders = Physics.OverlapSphere (_explosionPosition, radius, MaskedlayersTREENPC);   //  VERY VERY VERY FAR  ////    VERY VERY FAR shitting themselves
			if(BOMBNUKE){	if(NUKE){	 

										_collidersVeryFar = Physics.OverlapSphere (_explosionPosition, (radius / 1.5f), Maskedlayers);  //  VERY VERY FAR  ////    FAR AWAY BUT IN PAIN
										_collidersFARpanic = Physics.OverlapSphere (_explosionPosition, (radius * 2f), MaskedlayersNPC);
									  
									   // Debug.Log("NUKE");
									}
							if(!NUKE){	_collidersVeryFar = Physics.OverlapSphere (_explosionPosition, radius, Maskedlayers);  //  VERY VERY FAR  ////    FAR AWAY BUT IN PAIN
										_collidersFARpanic = Physics.OverlapSphere (_explosionPosition, (radius * 4f), MaskedlayersNPC);
										//Debug.Log("BOMB");

									}
						}
			if(!BOMBNUKE){			//_collidersVeryFar = Physics.OverlapSphere (_explosionPosition, radius * 1.1f, Maskedlayers);  //  VERY VERY FAR  ////    FAR AWAY BUT IN PAIN
									_collidersFARpanic = Physics.OverlapSphere (_explosionPosition, (radius * 7f), MaskedlayersNPC);
									    //Debug.Log("AA FIRE");

						}
						
			///////_collidersVeryFar = Physics.OverlapSphere (_explosionPosition, radius * 1.1f, Maskedlayers);  //  VERY VERY FAR  ////    FAR AWAY BUT IN PAIN
			//_collidersMedium = Physics.OverlapSphere (_explosionPosition, (radius / 1.4f), Maskedlayers);   ///VERY FAR  ////    ON FIRE RANGE		DENT DESTROY -- Meshchange
			//_collidersNearFireZone = Physics.OverlapSphere (_explosionPosition, (radius / 1.5f), Maskedlayers);  ////    ON FIRE RANGE    			SEMI DESTROY
			//_collidersNear = Physics.OverlapSphere (_explosionPosition, (radius / 2.2f), Maskedlayers); ////   NEAR EXPLOSION OBJECTS     			 TOTAL DESTROY
			//if(NUKE){ _collidersNearDELETE = Physics.OverlapSphere (_explosionPosition, (radius / 7f), Maskedlayers);} ////   NEAR EXPLOSION OBJECTS     DELETE      DELETE      DELETE     			 TOTAL DESTROY
			///////_collidersFARpanic = Physics.OverlapSphere (_explosionPosition, (radius * 7f), MaskedlayersNPC); ////   NEAR EXPLOSION OBJECTS     DELETE      DELETE      DELETE     			 TOTAL DESTROY
			//_collidersNearSUB1 = Physics.OverlapSphere (_explosionPosition, (radius / 1.9f), Maskedlayers); ////   NEAR EXPLOSION OBJECTS     			 TOTAL DESTROY // SUB DESTROY like windows
			
			//Debug.Log("how many times run");	
			Ammount = Random.Range(0,2);

			if(BOMBNUKE){
			foreach (Collider hit in _collidersVeryFar) 
			{
				
				if (!hit)
				{
					Debug.Log("WHAT THE F1");

					continue;
				}


				if (hit.GetComponent<Rigidbody>())
				{ 

						// Debug.Log("EACH TIME 1");
						// Debug.Log(hit.gameObject.name);
						// continue;
						// Debug.Log("EACH TIME 2");

					float dist = Vector3.Distance(hit.gameObject.transform.position, transform.position);
						
						if(hit.CompareTag("Light")){
							var LG = hit.GetComponent<LightControl>();
						if(LG != null){
							if(PSonce3){
							LG.PlaySound = true;
							PSonce3 = false;
							}
							if(!NUKE){LG.NoPower();continue;}
							if(NUKE){LG.NoPowerNuke();}
						continue;

					}}
					if(NUKE){
				
				if(lkt){
				  if(dist < (radius / 3.8f)){
					if(hit.CompareTag("DoorWindow")){	hit.gameObject.SetActive(false); 
					LevelControl.GetComponent<LevelPointControl>().Casulties += (Ammount + Ammount + Ammount);
					LevelControl.GetComponent<LevelPointControl>().CasultiescounterMIL += Ammount;
					LevelControl.GetComponent<LevelPointControl>().CasultiescounterCIV += (Ammount + Ammount);

					LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(60,200);
					LevelControl.GetComponent<LevelPointControl>().UpdateStats();

					continue;
					}

					if(hit.CompareTag("CARpart")){hit.gameObject.SetActive(false);
						if(Random.Range(1,6) == 2){GameObject FireUp = Instantiate(Resources.Load ("FIRElargeAfterMath") as GameObject,hit.transform.position,transform.rotation);continue;}

					}

					
					if(hit.CompareTag("WALL")){  hit.gameObject.SetActive(false); continue;  }
					if(hit.CompareTag("ROOF")){  hit.gameObject.SetActive(false); continue;  }

					}}

				
				if(lSkt){
				  if(dist < (radius / 3.5f)){
			if(hit.CompareTag("DoorWindow")){	hit.gameObject.SetActive(false); 
					LevelControl.GetComponent<LevelPointControl>().Casulties += (Ammount + Ammount + Ammount);
					LevelControl.GetComponent<LevelPointControl>().CasultiescounterMIL += Ammount;
					LevelControl.GetComponent<LevelPointControl>().CasultiescounterCIV += (Ammount + Ammount);

					LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(60,200);
					LevelControl.GetComponent<LevelPointControl>().UpdateStats();
					continue;
					}


					if(hit.CompareTag("CARpart")){hit.gameObject.SetActive(false);
						if(Random.Range(1,6) == 2){GameObject FireUp = Instantiate(Resources.Load ("FIRElargeAfterMath") as GameObject,hit.transform.position,transform.rotation);continue;}

					}

					
					if(hit.CompareTag("WALL")){  hit.gameObject.SetActive(false); continue;  }
					if(hit.CompareTag("ROOF")){  hit.gameObject.SetActive(false); continue;  }

					}}

				
				if(lMt){
				  if(dist < (radius / 3.3f)){
			if(hit.CompareTag("DoorWindow")){	hit.gameObject.SetActive(false); 
					LevelControl.GetComponent<LevelPointControl>().Casulties += (Ammount + Ammount + Ammount);
					LevelControl.GetComponent<LevelPointControl>().CasultiescounterMIL += Ammount;
					LevelControl.GetComponent<LevelPointControl>().CasultiescounterCIV += (Ammount + Ammount);

					LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(60,200);
 					LevelControl.GetComponent<LevelPointControl>().UpdateStats();

					continue;
					
					}


					if(hit.CompareTag("CARpart")){hit.gameObject.SetActive(false);
						if(Random.Range(1,6) == 2){GameObject FireUp = Instantiate(Resources.Load ("FIRElargeAfterMath") as GameObject,hit.transform.position,transform.rotation);continue;}

					}

					
					if(hit.CompareTag("WALL")){  hit.gameObject.SetActive(false); continue;  }
					//if(hit.CompareTag("ROOF")){  hit.gameObject.SetActive(false); Debug.Log("ROOF DELETE"); continue;  }

					}}

				if(S0Mt){
				  if(dist < (radius / 3.8f)){
					if(hit.CompareTag("DoorWindow")){	hit.gameObject.SetActive(false); continue;  }

					if(hit.CompareTag("CARpart")){hit.gameObject.SetActive(false);
						if(Random.Range(1,6) == 2){GameObject FireUp = Instantiate(Resources.Load ("FIRElargeAfterMath") as GameObject,hit.transform.position,transform.rotation);continue;}

					}
					if(hit.CompareTag("ROOF")){  hit.gameObject.SetActive(false);  continue;  }

					
					if(hit.CompareTag("WALL")){  hit.gameObject.SetActive(false); continue;  }

					}}




			}



//////   NEAR EXPLOSION OBJECTS   TOTAL DESTROY
//////   NEAR EXPLOSION OBJECTS   TOTAL DESTROY
//////   NEAR EXPLOSION OBJECTS   TOTAL DESTROY
//////   NEAR EXPLOSION OBJECTS   TOTAL DESTROY


					//_collidersVeryFar.list


				   ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				   ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


				  if(dist >= 0 && dist <= (radius / 1.9f)){


					


				if(BOMBNUKE == true){
				if(hit.CompareTag("DoorWindow")){  //////////////////   L1 of 5      TOTAL DESTROY
					///Debug.Log("DestroyWindow PLZ");
					var TS = hit.gameObject.GetComponent<WindowDoor>();
					if(!NUKE){if(TS != null){
						//hit.gameObject.layer = LayerMask.NameToLayer("ADDforce");
						if(TS.Door == false){
							if(Random.Range(1,3) == 1){
								LevelControl.GetComponent<LevelPointControl>().Casulties += (Ammount + Ammount + Ammount);
								LevelControl.GetComponent<LevelPointControl>().CasultiescounterMIL += Ammount;
								LevelControl.GetComponent<LevelPointControl>().CasultiescounterCIV += (Ammount + Ammount);
								LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(60,300);
								LevelControl.GetComponent<LevelPointControl>().UpdateStats();

							}

						if(PlaySoundOnceW){
							PlaySoundOnceW = false;
						TS.AudioTF = true;
						}}
						if(TS.Door == true){
							hit.gameObject.layer = LayerMask.NameToLayer("ADDforce");

						}
						
						TS.DestroyTotal();
						TS.DestroyDone = true;

					} continue;}
					if(NUKE){if(TS != null){
						//hit.gameObject.layer = LayerMask.NameToLayer("ADDforce");
						if(TS.Door == false){
								LevelControl.GetComponent<LevelPointControl>().Casulties += (Ammount + Ammount + Ammount);
								LevelControl.GetComponent<LevelPointControl>().CasultiescounterMIL += Ammount;
								LevelControl.GetComponent<LevelPointControl>().CasultiescounterCIV += (Ammount + Ammount);
								LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(60,200);
								LevelControl.GetComponent<LevelPointControl>().UpdateStats();

						if(PlaySoundOnceW){
							PlaySoundOnceW = false;
						TS.AudioTF = true;
						}}
						if(TS.Door == true){
							hit.gameObject.layer = LayerMask.NameToLayer("ADDforce");

						}
						TS.DestroyTotalNuke();
						TS.DestroyDone = true;

					}}
continue;
				}
				
				if(hit.CompareTag("Tree")){
						var TS = hit.gameObject.GetComponent<TreeExplodeControl>();

							if(PlaySoundOnce){
							PlaySoundOnce = false;
						TS.AudioTF = true;
						}

							TS.DestroyTree();
							hit.GetComponent<Rigidbody>().isKinematic = false;
							hit.GetComponent<Rigidbody>().useGravity = true;
							hit.gameObject.layer = LayerMask.NameToLayer("ADDforce");

							if(NUKE){
							TS.DestroyTreeFire();
							}
							continue;
						}
				
				
				
				if(hit.CompareTag("Metalsurface")){
					
					//var TS = hit.gameObject.GetComponent<TEMP>();
					if(hit.gameObject.GetComponent<Typeofhit>() != null){
				
				if(NUKE){
				if(hit.gameObject.GetComponent<Typeofhit>().GarBarrle == true){
							if(hit.gameObject.GetComponent<Typeofhit>().OilTankBig != true){
							hit.gameObject.SetActive(false);

								LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(100,300);

							if(NukeRepExpSmall != null){
							Instantiate(NukeRepExpSmall, hit.transform.position, Quaternion.Euler(0,0,0));
							}
							LevelControl.GetComponent<LevelPointControl>().OildrumsDestroyed += 1;continue;
				}}}
				
							if(hit.gameObject.activeSelf){
							//Debug.Log(hit.gameObject + "   BARRLE RUN 2, EXPLODE NORMAL");
							//hit.gameObject.GetComponent<Typeofhit>().AlreadyDestroyed = true;
							hit.gameObject.GetComponent<Typeofhit>().TakeMajorDamage();
							//hit.gameObject.SetActive(false);
				
							}
				
						if(hit.gameObject.GetComponent<IndustrialObject>() != null){       						 
					hit.gameObject.GetComponent<IndustrialObject>().TotalExplo();
								LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(550,1200);continue;
				}


				}}




				if(hit.CompareTag("WALL")){
					var TS = hit.gameObject.GetComponent<Wallcontrol>();
					if(TS != null){  //////////////////   L1 of 5      TOTAL DESTROY

						if(!NUKE){ TS.DestroyWalltotal();TS.DestroyDone = true; 
						if(LevelControl != null){
						 LevelControl.GetComponent<LevelPointControl>().Casulties += Ammount;
            			 LevelControl.GetComponent<LevelPointControl>().CasultiescounterCIV += Ammount;

						LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(111,333);
											LevelControl.GetComponent<LevelPointControl>().UpdateStats();}
						continue;}
						if(NUKE){ TS.DestroyWalltotalNuke();TS.DestroyDone = true; 
						LevelControl.GetComponent<LevelPointControl>().Casulties += Ammount;
            			LevelControl.GetComponent<LevelPointControl>().CasultiescounterCIV += Ammount;

						LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(111,444);
						LevelControl.GetComponent<LevelPointControl>().UpdateStats();

						continue;}

					}continue;
				}
				
				
				if(hit.CompareTag("ROOF")){
					if(NUKE){
						 if(hit.GetComponent<SegmentControl>() != null){
						 hit.GetComponent<SegmentControl>().SegmentDestroy();  
						 if(LevelControl != null){     //////////////////   L3 of 5
						 LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(550,1200);continue;
					}}}
					var TS = hit.gameObject.GetComponent<RoofControl>();
					if(TS != null){

						if(!NUKE){ TS.DestroyTotal();TS.DestroyDone = true; LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(111,333);continue;}
						if(NUKE){ TS.DestroyTotalNuke();TS.DestroyDone = true; LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(111,333);continue;
						

						}
					}
						
					//	var TS = hit.gameObject.GetComponent<TEMP>();
						if(hit.GetComponent<SegmentControl>() != null){
									//Debug.Log("ROOF DETECTED SUB");
							hit.GetComponent<SegmentControl>().SegmentSemiDes();LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(550,1200);continue;       //////////////////   L3 of 5
						}
					continue;
				}

					if(hit.CompareTag("CARpart")){     //////////////////   L1 of 5      TOTAL DESTROY
					var CP =  hit.gameObject.GetComponent<Rigidbody>();
					hit.transform.parent = null;
					hit.gameObject.layer = LayerMask.NameToLayer("ADDforce");
					hit.gameObject.GetComponent<BoxCollider>().isTrigger = false;
					CP.isKinematic = false;
					CP.useGravity = true;
					
					int LayerIgnoreRaycast = LayerMask.NameToLayer("ADDforce");
					hit.gameObject.layer = LayerIgnoreRaycast;
					
					var TS = hit.gameObject.GetComponent<LEDsign>();

					if(TS != null){
					TS.StopAllblinks(); continue;}
					
					//var TS = hit.gameObject.GetComponent<TEMP>();

					if(hit.gameObject.GetComponent<CARpartExtras>() != null){
						hit.gameObject.GetComponent<CARpartExtras>().RunExtraOptions();continue;
					}
					
					//var TS = hit.gameObject.GetComponent<TEMP>();

					if(hit.gameObject.GetComponent<TyresPop>() != null){
						hit.gameObject.GetComponent<TyresPop>().RunExtraOptions();
					continue;}

					if(hit.gameObject.GetComponent<PowerStationMain>() != null){										       
					hit.gameObject.GetComponent<PowerStationMain>().TotalDestroy();LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(444,988);continue;
				}
					
					continue;
					}

				
				

				if(hit.CompareTag("CAR")){
					
					//var TS = hit.gameObject.GetComponent<TEMP>();

					if(hit.gameObject.GetComponent<VEHICLEcontrol>() != null){
						if(NukeRepExpSmall != null){
							hit.gameObject.GetComponent<VEHICLEcontrol>().NoSurvivors = false;
							hit.gameObject.SetActive(false);
							Instantiate(NukeRepExpSmall, hit.transform.position, Quaternion.Euler(0,0,0));
							if(LevelControl.GetComponent<LevelPointControl>() != null){
            				LevelControl.GetComponent<LevelPointControl>().CARtruckcounter += 1;}
							LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(333,1200);
							continue;
						}
						else{ hit.gameObject.GetComponent<VEHICLEcontrol>().HealthHitBig();
					}
					if(NUKE){ hit.gameObject.GetComponent<VEHICLEcontrol>().HealthHitBMassive();}

				}continue;}

				//var TS = hit.gameObject.GetComponent<TEMP>();
				if(hit.gameObject.GetComponent<TroopTruck>() != null){
					hit.gameObject.GetComponent<TroopTruck>().TakeDamageTotal();LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(650,1200);continue;
				}

				

				if(hit.CompareTag("CarWindow")){  //////////////////   L1 of 5      TOTAL DESTROY
				//Debug.Log("CAR window hit");
					if(hit.gameObject.GetComponent<WindscreenControl>() != null){
					Instantiate(Resources.Load ("GlassShrapnel") as GameObject,hit.transform.position,transform.rotation);
					continue;					
										}continue;}
							
				
				if(hit.CompareTag("FenceHedge")){
					hit.gameObject.GetComponent<FenceHegWallcontrol>().DestroyFencetotal();
					LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(50,120);continue;				
				}

				if(hit.CompareTag("SimpleObj")){
				if(hit.gameObject.GetComponent<ObjectSubTotalDes>() != null){
					hit.gameObject.GetComponent<ObjectSubTotalDes>().DestroyObjtotal();		
					LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(50,200);continue;
					}			
				continue;}
				
						
				if(hit.CompareTag("BuildingSegment")){
						if(hit.gameObject.GetComponent<SegmentEffect>() != null){
//							Debug.Log("Segmant Destroy");
					hit.gameObject.GetComponent<SegmentEffect>().DestroyObjtotal();	LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(650,1200);continue;	}			
				}
				if(hit.CompareTag("PowerS2")){
				if(hit.gameObject.GetComponent<PowerSegment>() != null){                                 
				if(hit.gameObject.GetComponent<PowerSegment>().DirectExpEffect == true){
					hit.gameObject.GetComponent<PowerSegment>().TotalDestroy();continue;
				}}}


					if(hit.CompareTag("GasCap")){
						if(Random.Range(1,4) == 2){
                if(hit.GetComponentInParent<Typeofhit>() != null){
               hit.GetComponentInParent<Typeofhit>().EnableGasLeak(); }
					continue;}}


				if(hit.CompareTag("TrainCab")){
					hit.gameObject.GetComponent<TrainControl>().TrainTotalDestroy();
					LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(2450,3200);					
					continue;
				}	
				if(hit.CompareTag("TrainWheel")){  //////////////////   L1 of 5      TOTAL DESTROY
					hit.gameObject.SetActive(false);	continue;				
				}		

				if(hit.CompareTag("INFOcube")){
						//Debug.Log("INFO CUBE DETECTED");
						if(hit.gameObject.GetComponent<LEVELtargets>() != null){
							hit.gameObject.GetComponent<LEVELtargets>().TargetKilledRescueCapture();
						}continue;
				}
				
				
				if(hit.CompareTag("SAMsite")){
						if(hit.gameObject.GetComponent<TurretControl>() != null){
							hit.gameObject.GetComponent<TurretControl>().TotalDestroySam();continue;

						}continue;
				}



				
				// if(hit.GetComponent<TurretControl>() != null){
				// 			hit.GetComponent<TurretControl>().TurretHealth -= 200;       //////////////////   L3 of 5
				// 	continue;}
									if(hit.CompareTag("Metalsurface")){

						if(hit.gameObject.GetComponent<Typeofhit>() != null){                                  
							if(hit.gameObject.activeSelf){
					hit.gameObject.GetComponent<Typeofhit>().TotalDestroy();
							}
				continue;
				}		
			
				}
						if(hit.CompareTag("Plane")){

										if(hit.gameObject.GetComponent<Planemove>() != null){
					hit.gameObject.GetComponent<Planemove>().Crashthisplane();
					hit.gameObject.GetComponent<Planemove>().PlaneHealth = 0;
					continue;
				}
										if(hit.gameObject.GetComponent<ChopperMove>() != null){
					hit.gameObject.GetComponent<ChopperMove>().PlaneHealth = 0;
					continue;
				}	

				}}
				}
			
//////    ON FIRE RANGE   ///  SEMIE DESTROY
//////    ON FIRE RANGE   ///  SEMIE DESTROY
//////    ON FIRE RANGE   ///  SEMIE DESTROY
//////    ON FIRE RANGE   ///  SEMIE DESTROY


				  if(dist >= (radius / 20f) && dist <= (radius / 1.4f)){

					
					//Debug.Log("TEST 1");
					if(BOMBNUKE == true){

						//var TS = hit.gameObject.GetComponent<TEMP>();
						if(hit.CompareTag("Tree")){
							if(PlaySoundOnce){
							PlaySoundOnce = false;
						hit.gameObject.GetComponent<TreeExplodeControl>().AudioTF = true;
						}
						hit.GetComponent<TreeExplodeControl>().ActivateParticals();
						hit.GetComponent<TreeExplodeControl>().EnableRidgedbody();
						hit.gameObject.layer = LayerMask.NameToLayer("ADDforce");
						continue;
					}


					
					if(hit.CompareTag("WALL")){
						//var TS = hit.gameObject.GetComponent<TEMP>();
					if(hit.gameObject.GetComponent<Wallcontrol>() != null){
						hit.gameObject.GetComponent<Wallcontrol>().DestroyWallsemi();
						if(LevelControl != null){
						 LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(90,200);
						 LevelControl.GetComponent<LevelPointControl>().Casulties += (Ammount);
            			 LevelControl.GetComponent<LevelPointControl>().CasultiescounterCIV += (Ammount);
 						 LevelControl.GetComponent<LevelPointControl>().UpdateStats();
						}
						continue;
					}
				}
				
				if(hit.CompareTag("DoorWindow")){               //////////////////   L2 of 5      ///  SEMIE DESTROY
				//var TS = hit.gameObject.GetComponent<TEMP>();
					if(hit.gameObject.GetComponent<WindowDoor>() != null){
						if(hit.gameObject.GetComponent<WindowDoor>().Door == false){
								if(Random.Range(1,3) == 1){
									if(LevelControl != null){
								LevelControl.GetComponent<LevelPointControl>().Casulties += Ammount;
								LevelControl.GetComponent<LevelPointControl>().CasultiescounterCIV += Ammount;
								LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(60,200);
								LevelControl.GetComponent<LevelPointControl>().UpdateStats();

								}}

						if(PlaySoundOnceW1){
							PlaySoundOnceW1 = false;
						hit.gameObject.GetComponent<WindowDoor>().AudioTF = true;

						}}
												
						hit.gameObject.GetComponent<WindowDoor>().DestroySemi();

						continue;
					}
				 }
				if(hit.CompareTag("ROOF")){
							//Debug.Log("ROOF DETECTED");
				//var TS = hit.gameObject.GetComponent<TEMP>();
					if(hit.gameObject.GetComponent<RoofControl>() != null){
						hit.gameObject.GetComponent<RoofControl>().DestroyDone = true;
						hit.gameObject.GetComponent<RoofControl>().DestroyTotal();
						LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(90,266);continue;
					}
				
								if(hit.GetComponent<SegmentControl>() != null){
								//	Debug.Log("ROOF DETECTED SUB");
							hit.GetComponent<SegmentControl>().SegmentSemiDes();
							LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(400,899);continue;
							       //////////////////   L3 of 5
						}
					}

				//var TS = hit.gameObject.GetComponent<TEMP>();
				if(hit.CompareTag("CAR")){
					if(hit.gameObject.GetComponent<VEHICLEcontrol>() != null){
						if(hit.gameObject.activeSelf){
							if(hit.gameObject.GetComponent<VEHICLEcontrol>().BoatShip != true){
						hit.gameObject.GetComponent<VEHICLEcontrol>().EngineDie();
						LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(111,333);continue;
						}
							if(hit.gameObject.GetComponent<VEHICLEcontrol>().BoatShip == true){
								hit.gameObject.GetComponent<VEHICLEcontrol>().HealthHitBig();continue;
							}

						if(hit.GetComponentInChildren<GasTankexp>() != null){
						hit.GetComponentInChildren<GasTankexp>().Gastankhealth -= 1111;
						hit.GetComponentInChildren<GasTankexp>().GasTankHit();
					}}}continue;
				}


				if(hit.CompareTag("CARpart")){
				if(hit.gameObject.GetComponent<CARpartExtras>() != null){												       
						hit.gameObject.GetComponent<CARpartExtras>().RunSomeExtraOptionsSEMI();continue;
					}
					if(hit.gameObject.GetComponent<PowerStationMain>() != null){										       
					hit.gameObject.GetComponent<PowerStationMain>().TotalDestroy();continue;
				}
				}

				if(hit.CompareTag("Metalsurface")){
					if(hit.gameObject.GetComponent<TroopTruck>() != null){											        
					hit.gameObject.GetComponent<TroopTruck>().TakeSemiDestroy();continue;
				}

				if(hit.GetComponent<Typeofhit>() != null){ 
				             //////////////////   L2 of 5      ///  SEMIE DESTROY             
							hit.GetComponent<Typeofhit>().TakeDamage();
							if(NUKE){
							hit.GetComponent<Typeofhit>().TakeMajorDamage();
							}
				continue;}}

			
					if(hit.CompareTag("TrainWheel")){
					hit.gameObject.GetComponent<TrainWheelControl>().SemiDamage();	continue;				
				}	
					if(hit.CompareTag("FenceHedge")){
					hit.gameObject.GetComponent<FenceHegWallcontrol>().DestroyFencesemi();	
					LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(40,80);continue;
;				
				}
					if(hit.CompareTag("SimpleObj")){
						if(hit.gameObject.GetComponent<ObjectSubTotalDes>() != null){					
					hit.gameObject.GetComponent<ObjectSubTotalDes>().DestroyObjsemi();}		continue;			
				}
					if(hit.CompareTag("BuildingSegment")){
						if(hit.gameObject.GetComponent<SegmentEffect>() != null){
					hit.gameObject.GetComponent<SegmentEffect>().SemiDamage();
					LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(555,888);continue;
	
						}		continue;	
				}


								if(hit.CompareTag("Fallover")){
					if(hit.gameObject.GetComponent<BigStructureBrake>() != null){
						hit.gameObject.GetComponent<BigStructureBrake>().DestroyTotal();
						hit.gameObject.GetComponent<BigStructureBrake>().DestroyDone = true;

					}continue;
				}
					if(hit.CompareTag("GasCap")){
						if(Random.Range(1,4) == 2){

                if(hit.GetComponentInParent<Typeofhit>() != null){
                hit.GetComponentInParent<Typeofhit>().EnableGasLeak(); }
					continue;}
				}

				if(hit.CompareTag("ParShoot")){
				if(hit.gameObject.GetComponent<Parashootcontrol>()!=null){										        
					hit.gameObject.GetComponent<Parashootcontrol>().CatchFire();continue;
				}}

				if(hit.CompareTag("PowerS")){
						if(hit.gameObject.GetComponent<PowerStationMain>() != null){										       
					hit.gameObject.GetComponent<PowerStationMain>().TotalDestroy();
					LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(444,666);continue;

				}
				}


if(hit.CompareTag("Plane")){
						if(hit.gameObject.GetComponent<Planemove>() != null){												        
					hit.gameObject.GetComponent<Planemove>().Crashthisplane();
										hit.gameObject.GetComponent<Planemove>().PlaneHealth = 0;continue;

				}

				
					if(hit.GetComponent<ChopperMove>() != null){              //////////////////   L2 of 5      ///  SEMIE DESTROY             
							hit.GetComponent<ChopperMove>().PlaneHealth = 0;
							hit.GetComponent<ChopperMove>().CheckChopperHealth();continue;

							
				}}
								if(hit.CompareTag("CarWindow")){  //////////////////   L1 of 5      TOTAL DESTROY
				//Debug.Log("CAR window hit");
					if(hit.gameObject.GetComponent<WindscreenControl>() != null){
					Instantiate(Resources.Load ("GlassShrapnel") as GameObject,hit.transform.position,transform.rotation);continue;				
										}}


					if(hit.CompareTag("TrainCab")){
					hit.gameObject.GetComponent<TrainControl>().TrainSemiDestroy();	
					continue;				
				}
				if(hit.CompareTag("Nature")){
					 if(hit.gameObject.GetComponent<FlamingoDie>() != null){									       
					hit.gameObject.GetComponent<FlamingoDie>().NatureDie();continue;
				}}



				}
				

				if(NUKE){
				if(hit.CompareTag("TrainCab")){
					hit.gameObject.GetComponent<TrainControl>().TrainTotalDestroy();continue;
					
				}
				if(hit.CompareTag("TrainWheel")){              //////////////////   L2 of 5      ///  SEMIE DESTROY
					hit.gameObject.GetComponent<TrainWheelControl>().SemiDamage();		continue;			
									
				}

	
				}


					
				}

//////    VERY VERY FAR shitting themselves
//////    VERY VERY FAR shitting themselves
//////    VERY VERY FAR shitting themselves
//////    VERY VERY FAR shitting themselves



				  if(dist >= 0 && dist <= (radius / 1.2f)){


						if(BOMBNUKE){
			if(hit.CompareTag("DoorWindow")){               //////////////////   L2 of 5      ///  SEMIE DESTROY
				//var TS = hit.gameObject.GetComponent<TEMP>();
					if(hit.gameObject.GetComponent<WindowDoor>() != null){
						if(hit.gameObject.GetComponent<WindowDoor>().Door == false){
								if(Random.Range(1,3) == 2){
								if(LevelControl != null){
								LevelControl.GetComponent<LevelPointControl>().Casulties += Ammount;
								LevelControl.GetComponent<LevelPointControl>().CasultiescounterCIV += Ammount;
								LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(80,200);
								LevelControl.GetComponent<LevelPointControl>().UpdateStats();
								}}

						if(PlaySoundOnceW1){
							PlaySoundOnceW1 = false;
						hit.gameObject.GetComponent<WindowDoor>().AudioTF = true;

						}}
												
						hit.gameObject.GetComponent<WindowDoor>().DestroySemi();

						continue;
					}
				 }



					if(hit.CompareTag("ROOF")){
					//	Debug.Log("ROOF DETECT 2");
					//var TS = hit.gameObject.GetComponent<TEMP>();
					if(hit.gameObject.GetComponent<RoofControl>() != null){
						hit.gameObject.GetComponent<RoofControl>().DestroyDone = true;
						hit.gameObject.GetComponent<RoofControl>().DestroyDent();
					continue;}
				
					if(hit.GetComponent<SegmentControl>() != null){                                     
							hit.GetComponent<SegmentControl>().SegmentSemiDes();       //////////////////   L3 of 5
						continue;}
					}



						if(hit.CompareTag("Tree")){
						//	var TS = hit.gameObject.GetComponent<TEMP>();
							if(PlaySoundOnce){
							PlaySoundOnce = false;
						hit.gameObject.GetComponent<TreeExplodeControl>().AudioTF = true;
						}
						hit.GetComponent<TreeExplodeControl>().ActivateParticals();
						hit.GetComponent<TreeExplodeControl>().EnableRidgedbody();
						hit.gameObject.layer = LayerMask.NameToLayer("ADDforce");
						continue;
					}

									if(hit.CompareTag("PowerS")){
							if(hit.gameObject.GetComponent<PowerStationMain>() != null){                
					hit.gameObject.GetComponent<PowerStationMain>().SemiDestroy();LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(222,444);continue;
				}}

					if(hit.CompareTag("Metalsurface")){
						if(hit.GetComponent<Typeofhit>() != null){              					     
							if(hit.gameObject.activeSelf){
							hit.GetComponent<Typeofhit>().TakeDamage();
							}}
							continue;
							}

				
				
					if(NUKE){

				if(hit.CompareTag("TrainCab")){
					hit.gameObject.GetComponent<TrainControl>().TrainSemiDestroy();continue;					
				}
						

					if(hit.CompareTag("TrainWheel")){
					hit.gameObject.GetComponent<TrainWheelControl>().SemiDamage();	continue;				
				}
								if(hit.CompareTag("SimpleObj")){
				if(hit.gameObject.GetComponent<ObjectSubTotalDes>() != null){
					hit.gameObject.GetComponent<ObjectSubTotalDes>().DestroyObjtotal();	continue;	}			
				}	

					
				}

					if(hit.CompareTag("Missile")){
						if(NUKE){hit.GetComponent<AirStrike>().ExplodeNuke();LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(444,988);continue;}
					hit.GetComponent<AirStrike>().Explode();LevelControl.GetComponent<LevelPointControl>().EconomyCost += Random.Range(333,555);       //////////////////   L3 of 5
					continue;}
				
				
				}


				}

						if(dist >= (radius / 20f) && dist <= (radius * 1.2f)){
							if(BOMBNUKE){
						if(hit.CompareTag("Tree")){
						hit.GetComponent<TreeExplodeControl>().ActivateParticals();
						hit.GetComponent<Rigidbody>().useGravity = true;
						hit.GetComponent<Rigidbody>().isKinematic = false;
						hit.gameObject.layer = LayerMask.NameToLayer("ADDforce");
						continue;
					}
									if(hit.CompareTag("Plane")){
					if(hit.gameObject.GetComponent<Planemove>() != null){
					hit.gameObject.GetComponent<Planemove>().Crashthisplane();
										hit.gameObject.GetComponent<Planemove>().PlaneHealth = 0;
					continue;
				}	
						if(hit.GetComponent<ChopperMove>() != null){             
					hit.GetComponent<ChopperMove>().PlaneHealth -= 1110;
					hit.GetComponent<ChopperMove>().CheckChopperHealth();
					continue;
							
				}}
			 
					}}
				}
				
			}

			}

///                                  NPC  CONTROL
 
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
///    

	
				foreach (Collider hit in _collidersFARpanic) 
				{
				float dist = Vector3.Distance(hit.gameObject.transform.position, transform.position);

				if (!hit)
				{
					continue;
				}
				if (hit.gameObject.activeInHierarchy){
				if (hit.GetComponent<Rigidbody>())
				{

				var NPC = hit.gameObject.GetComponent<NPCcontrol>();
				var PC = hit.gameObject.GetComponent<LevelPointControl>();
/////   NPC  CONTROLS 
if(! TOXIC){
				if(NUKE){

					if(dist >= (radius / 20f) && dist <= (radius / 3.7f)){
						if(NPC != null){
						hit.gameObject.SetActive(false);
						//Debug.Log("ONCE ONLY");
						if(Random.Range(1,5) == 2){GameObject FireUp = Instantiate(Resources.Load ("FIRElargeAfterMath") as GameObject,hit.transform.position,transform.rotation);}
						if(PC != null){
						PC.CasultiescounterCIV += 1;
            			PC.Casulties += 1;	}	
						LevelControl.GetComponent<LevelPointControl>().UpdateStats();

						continue;				
					}
						if(hit.GetComponent<Crowdcontrol>() != null){
						hit.gameObject.SetActive(false);
						if(PC != null){
						PC.CasultiescounterCIV += 2;
            			PC.Casulties += 2;	}	
						LevelControl.GetComponent<LevelPointControl>().UpdateStats();

						continue;				
					}
				}

				  if(dist >= (radius / 3.7f) && dist <= (radius / 2.5f)){
						if(NPC != null){
						hit.gameObject.SetActive(false);
						if(NukeRepBloodSplat != null){
							Instantiate(NukeRepBloodSplat, hit.transform.position, Quaternion.Euler(0,0,0));
							if(Ragdoll != null){
								if(Random.Range(1,3) == 2){
									Instantiate(Ragdoll, hit.transform.position, Quaternion.Euler(0,0,0));

								}
							}
						}
						if(PC != null){
						PC.CasultiescounterCIV += 1;
            			PC.Casulties += 1;		
						LevelControl.GetComponent<LevelPointControl>().UpdateStats();

						continue;				
					}}
						if(hit.GetComponent<Crowdcontrol>() != null){
						hit.gameObject.SetActive(false);
						if(NukeRepBloodSplat != null){
							Instantiate(NukeRepBloodSplat, hit.transform.position, Quaternion.Euler(0,0,0));
						}
						if(PC != null){
						PC.CasultiescounterCIV += 2;
            			PC.Casulties += 2;
						LevelControl.GetComponent<LevelPointControl>().UpdateStats();

						continue;				
					}}
				}}


				  if(dist >= (radius / 20f) && dist <= (radius / 1.6f)){
						if(NPC != null){
						if(PSonce2){
							NPC.PlaySound = true;
							PSonce2 = false;
						}
						if(NUKE){
							if(lSkt){ NPC.BPchance = Random.Range(1,3);}
							if(lMt){ NPC.BPchance = Random.Range(1,4);}
							if(S0Mt){ NPC.BPchance = Random.Range(1,5);}
							
						}
						NPC.DEADman();
						if(Random.Range(1,4) == 2){
							if(NUKE){ GameObject F1 = Instantiate(Resources.Load ("FIREsmall") as GameObject,hit.transform.position,transform.rotation);continue;}



							GameObject F2 = Instantiate(Resources.Load ("FireBit1") as GameObject,hit.transform.position,transform.rotation);

							
						}
						

					continue;}
						var CC = hit.gameObject.GetComponent<Crowdcontrol>();

						if(CC != null){
						if(PSonce1){
							PSonce1 = false;
						CC.PlaySound = true;
						}
						CC.DeathKill();
						if(!BOMBNUKE){GameObject CP1 = Instantiate(Resources.Load ("CrowdPop") as GameObject,hit.transform.position,transform.rotation);continue;}
						if(BOMBNUKE){if(Random.Range(1,4) == 2){GameObject CP1 = Instantiate(Resources.Load ("CrowdPopNL") as GameObject,hit.transform.position,transform.rotation);}}

						
						continue;}

				   } 

				   ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				   ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

				  if(dist >= (radius / 1.6f) && dist <= (radius / 1.2f)){ 
						
						if(NPC != null){     //////////////////   L2 of 5       ///  SEMIE DESTROY
						if(!BOMBNUKE){
							if(Random.Range(1,4) == 1){
						NPConFire = true;
						NPC.ManOnFire();
						NPC.DontMove = false; 
						}continue;}
						if(BOMBNUKE){
							if(Random.Range(1,3) == 2){
						NPConFire = true;
						NPC.ManOnFire();
						NPC.DontMove = false; 
						continue;}}
					}
					
				  }

				}

				   ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				   ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

				if(dist >= (radius / 20f) && dist <= (radius / 1.1f)){
						

				if(TOXIC){
							if(NPC != null){
						
								NPC.ToxicSpillEnable();
								NPC.DontMove = false; 
								NPC.ShittingMan();
							continue;

						 
					}}
				
				}


 ////////////////    MAXIMUM AREA AFFECT RANGE 1
			if(! TOXIC){
 				if(dist >= (radius / 1.7f) && dist <= (radius * 1.5f)){

				if(BOMBNUKE){
						if(NPC != null){
						NPC.DontMove = false; 
						NPC.ShittingMan();
						if(NUKE){
												
						if(Random.Range(1,7) == 1){	
						NPConFire = true;
						NPC.ManOnFire();
						}}

			continue;}
			}

							


			}
  
				if(dist >= (radius / 3.5f) && dist <= (radius * 7f)){
 				
				if(!BOMBNUKE){
						if(NPC != null){
						NPC.DontMove = false; 
						NPC.ShittingManDontDrop();
						continue;
						}

				
			}



			}

				}}
				}


}

	}

	// IEnumerator ST(){
	//   Time.timeScale = 0.1f;
	//   yield return new WaitForSeconds(0.04f);
	//   Time.timeScale = 0.9f;
	//   yield return new WaitForSeconds(12.15f);
	//   Time.timeScale = 1;

	// }



	IEnumerator WAS(){
	  yield return new WaitForSeconds(0.07f);

	  _collidersVeryFar = null;
	  _collidersVeryFar = Physics.OverlapSphere (_explosionPosition, radius / 2, MaskedlayersForce2);  //  VERY VERY FAR  ////    FAR AWAY BUT IN PAIN
	  foreach (Collider hit in _collidersVeryFar)  //////    VERY VERY FAR shitting themselves
			{
				if (!hit)
				{
					Debug.Log("WHAT THE F3");
					continue;
				}
				var H = hit.GetComponent<Rigidbody>();

				if (H){
					H.AddExplosionForce((power * size * (Random.Range(8,12))), _explosionPosition, (radius * size * 3.5f), (10f  * size));
					continue;
				}
	}


	  yield return new WaitForSeconds(0.03f);

	  _collidersVeryFar = null;
	  _collidersVeryFar = Physics.OverlapSphere (_explosionPosition, radius, MaskedlayersForce);  //  VERY VERY FAR  ////    FAR AWAY BUT IN PAIN
		


	  foreach (Collider hit in _collidersVeryFar)  //////    VERY VERY FAR shitting themselves
			{
				if (!hit)
				{
					Debug.Log("WHAT THE F4");
					continue;
				}
				var H = hit.GetComponent<Rigidbody>();

				if (H){

				H.AddExplosionForce((power * size * (Random.Range(1.15f,1.4f))), _explosionPosition, (radius * size * 4), (8f  * size));


			}

	}


	}

}

