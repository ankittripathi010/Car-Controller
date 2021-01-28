using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float steeringAngle;
    float speed = 20f;
    public float MaxSpeed =20f;
    private float distance;
    public int SpeedInKmph;
    public Text fuelText;
    public Text SpeedText;
    private float FuelDecreaseRate = 0.01f;
    public GameObject fuelTank;
   
   // public Canvas canv;
    public Vector3 com;
    
    private float Fuel = 20;
    private int fuelInt;
  //  private float mileage = 1;

    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;

    public Camera firstPerson , thirdPerson;
    public bool cam = false;
    

    public float maxSteerAngle = 30f;
    public float MotorForce = 50f;
    public float handBrake = 100000f;
    public Rigidbody rb;



    //Function for getting inputs for movements
    public void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }


    //for steering.
    public void steer()
    {
       
        steeringAngle = maxSteerAngle * horizontalInput;
        frontDriverW.steerAngle = steeringAngle;
        frontPassengerW.steerAngle = steeringAngle;
       
       
    }

    //for accelerating
    public void accelerate()
    {
        
        rearDriverW.motorTorque = verticalInput * MotorForce;
        rearPassengerW.motorTorque = verticalInput * MotorForce;
        if(Input.GetKeyDown(KeyCode.LeftShift)) //for using boost
        {
           // Debug.Log("boosting on");
            MaxSpeed = 70;

            rearDriverW.motorTorque = MotorForce + 1000;
            rearPassengerW.motorTorque = MotorForce + 1000;
        }

        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
           // Debug.Log("boosting off");
            MaxSpeed = 50;
        }
        if (rearDriverW.motorTorque > 0)
        {
            Fuel -= FuelDecreaseRate;   //fuel consumptions
            Debug.Log(Fuel);
        }
    }


    //will decelerate the vheicle once fuel ends.
    public void Decelerate()
    {
        rearDriverW.motorTorque = 0;
        rearPassengerW.motorTorque = 0;
       
    }


    //applying hand brake.
    public void HandBrake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("hand brake applied");
            rearDriverW.brakeTorque = handBrake;
            rearPassengerW.brakeTorque = handBrake;
            rearDriverW.motorTorque = 0;
            rearPassengerW.motorTorque = 0;
        }
        else
        {
            rearDriverW.brakeTorque = 0;
            rearPassengerW.brakeTorque = 0;
        }
    }

    //for switching from third person to first person and vice-versa
    public void CamSwitch()
    {
        
        if(Input.GetKeyDown(KeyCode.V))
        {
            cam = !cam;
            firstPerson.gameObject.SetActive(!cam);
            thirdPerson.gameObject.SetActive(cam);
        }
        
    }


    
    public void UpdateWheelPoses()
    {
        updateWheelPose(frontDriverW, frontDriverT);
        updateWheelPose(rearDriverW, rearDriverT);
        updateWheelPose(frontPassengerW, frontPassengerT);
        updateWheelPose(rearPassengerW, rearPassengerT);
    }

    //will update the inputs to the wheel geometry
    public void updateWheelPose(WheelCollider col, Transform trans)
    {
        Vector3 pos = trans.position;
        Quaternion quat = trans.rotation;
        

         col.GetWorldPose(out pos, out quat);
       
          trans.position = pos;
        trans.rotation = quat;
    }

   
   private void Start()
    {
        thirdPerson.gameObject.SetActive(true);
        firstPerson.gameObject.SetActive(false);
        rb.centerOfMass = com;
    }


    //for refuling 
   private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag  == "feul tank")
        {
             
            Fuel += 1;
            Destroy(fuelTank);
        }
    }

    

    private void FuelAndAcceleration()
    {

        speed = rb.velocity.magnitude;
        

        SpeedInKmph = (int)speed * 18 / 5;
        SpeedText.text = SpeedInKmph.ToString();
        if (speed >= MaxSpeed)
        {
       //     Debug.Log("Maximum speed reached");
            rb.velocity = rb.velocity.normalized * MaxSpeed;
        }
        distance += SpeedInKmph * Time.deltaTime/3600;


        
      //  Debug.Log(distance);
        if(Fuel > 0)
        {
            accelerate();
        }
        else
        {
            Debug.Log("out of feul");
            Decelerate();
        }
        fuelInt = (int)Fuel;
        fuelText.text = fuelInt.ToString();
    }

    private void FixedUpdate()
    {
        GetInput();
        steer();
        UpdateWheelPoses();
        CamSwitch();
        FuelAndAcceleration();
        HandBrake();
    }
}
