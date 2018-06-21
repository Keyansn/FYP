using UnityEngine;
using System.Collections;
using System;

public class Link : MonoBehaviour
{

    public string id;
    public GameObject source;
    public GameObject target;
	public float weight;
	public float weightscale;

    public bool direction;
    public static float intendedLinkLength = 1;
    public static float forceStrength = 3;

    //private static GameController gameControl;
    //private static GraphController graphControl;
	private Rigidbody thisRigidbody;
    private Component sourceRb;
    private Component targetRb;
    private LineRenderer lineRenderer;
    public Material material;//link material
    public Material hiddenmaterial;

    private float intendedLinkLengthSqr;
    private float distSqrNorm;

    public Color colorStart = Color.red;
    public Color colorEnd = Color.green;
    int i = 0;
    public int divider = 10;

    public bool randomColours = false;
    public bool materialRed = false;

    void doAttraction()
    {
        Vector3 forceDirection = sourceRb.transform.position - targetRb.transform.position;
        float distSqr = forceDirection.sqrMagnitude;
        //Debug.Log("distSqr: " + distSqr);
        if (distSqr > intendedLinkLengthSqr)
        {
            //Debug.Log("(Link.FixedUpdate) distSqr: " + distSqr + "/ intendedLinkLengthSqr: " + intendedLinkLengthSqr + " = distSqrNorm: " + distSqrNorm);
            distSqrNorm = distSqr / intendedLinkLengthSqr;

            Vector3 targetRbImpulse = forceDirection.normalized * forceStrength * distSqrNorm;
            Vector3 sourceRbImpulse = forceDirection.normalized * -1 * forceStrength * distSqrNorm;

            
            
            //Debug.Log("(Link.FixedUpdate) targetRb: " + targetRb + ". forceDirection.normalized: " + forceDirection.normalized + ". distSqrNorm: " + distSqrNorm + ". Applying Impulse: " + targetRbImpulse);
            ((Rigidbody)targetRb as Rigidbody).AddForce(targetRbImpulse);
            //Debug.Log("(Link.FixedUpdate) targetRb: " + sourceRb + ". forceDirection.normalized: " + forceDirection.normalized + "  * -1 * distSqrNorm: " + distSqrNorm + ". Applying Impulse: " + sourceRbImpulse);
            ((Rigidbody)sourceRb as Rigidbody).AddForce(sourceRbImpulse);
            
        }
    }

    // Use this for initialization
    void Start()
    {
		thisRigidbody = this.GetComponent<Rigidbody>();

        gameObject.layer = 8;

		//GlobalScalers Script = GetComponent<GlobalScalers>();
		weightscale = 1f;
		//weightscale = Script.weightScale;
		GlobalScalers Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GlobalScalers>();

		//print("Script.largestLink: " + Script.largestLink);
		weightscale = Script.largestLink;


        //gameControl = FindObjectOfType<GameController>();
        //graphControl = FindObjectOfType<GraphController>();
		//weightScale = 1;
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        //color link according to status
        Color c;
        c = Color.cyan;
        c.a = 0.5f;



        //draw line
        //lineRenderer.material = new Material(Shader.Find("Self-Illumin/Diffuse"));
        // lineRenderer.material = new Material(Shader.Find("Particles/Additive"));   //THIS WAS THE OG
        lineRenderer.material = material;
        ////lineRenderer.material.color = (c);


        //lineRenderer.material.color = Color.Lerp(colorStart, colorEnd, (Mathf.PingPong(Time.time, 1) / 1));

        //lineRenderer.startColor = (Color.red);
        //lineRenderer.endColor=(c);
        //lineRenderer.SetColors(Color.red, Color.cyan);

        /*
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.green, 0.0f), new GradientColorKey(Color.red, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );
        lineRenderer.colorGradient = gradient;
        */

		print("weight" + weight);
		print("weightScale" + weightscale);

        if (direction == true)
        {
			lineRenderer.startWidth = 0.5f*weight/weightscale;
			lineRenderer.endWidth = 0.1f*weight/weightscale;
            //lineRenderer.SetColors(Color.red, Color.cyan);
            lineRenderer.startColor = Color.blue;
            lineRenderer.endColor = Color.cyan;
        }
        else
        {
			lineRenderer.startWidth = weight/weightscale;
			lineRenderer.endWidth = weight/weightscale;
            //lineRenderer.SetColors(Color.red, Color.cyan);

            if (randomColours)
            {
                Color RandColor = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f);
                lineRenderer.startColor = RandColor;
                lineRenderer.endColor = RandColor;
            } 
            else if (materialRed == true)
            {
                Color matrd = MaterialRed(Convert.ToInt32(UnityEngine.Random.Range(100f, 900f)));
                lineRenderer.startColor = matrd;
                //colorStart = matrd;
                lineRenderer.endColor = matrd;
            }
            else
            {
                lineRenderer.startColor = Color.grey;
                lineRenderer.endColor = Color.grey;
            }




            //lineRenderer.startColor = Color.red;
            //lineRenderer.endColor = Color.red;
        }

        //lineRenderer.startWidth = 0.5f;
        //lineRenderer.endWidth = 0.1f;

        lineRenderer.positionCount = 4;
        lineRenderer.SetPosition(0, source.transform.position);
        lineRenderer.SetPosition(1, 0.75f * source.transform.position + 0.25f * target.transform.position);
        lineRenderer.SetPosition(2, 0.25f * source.transform.position + 0.75f * target.transform.position);
        lineRenderer.SetPosition(3, target.transform.position);


        sourceRb = source.GetComponent<Rigidbody>();
        targetRb = target.GetComponent<Rigidbody>();        

        intendedLinkLengthSqr = intendedLinkLength * intendedLinkLength;

        intendedLinkLength = GameObject.Find("GameController").GetComponent<GraphController>().LinkLength;
        forceStrength = GameObject.Find("GameController").GetComponent<GraphController>().forceStrength;
    }



    // Update is called once per frame
    void FixedUpdate()
    {


        InputToAction Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputToAction>();
            if (Script.frozen == false)
            {
                ////// NEED TO PUT THIS BACK IN EVENTUALLY
                //intendedLinkLength = GameObject.Find("GlobalController").GetComponent<GraphController>().LinkLength;
                //forceStrength = GameObject.Find("GlobalController").GetComponent<GraphController>().forceStrength;

                // moved from Start() in Update(), otherwise it won't see runtime updates of intendedLinkLength
                intendedLinkLengthSqr = intendedLinkLength * intendedLinkLength;

                lineRenderer.SetPosition(0, source.transform.position);
            lineRenderer.SetPosition(1, 0.75f * source.transform.position+ 0.25f * target.transform.position);
            lineRenderer.SetPosition(2, 0.25f * source.transform.position + 0.75f * target.transform.position);
            lineRenderer.SetPosition(3, target.transform.position);

            AnimationCurve curve = new AnimationCurve();
            
            curve.AddKey(0f, 0.4f);
            curve.AddKey(0.2f, 0.1f);
            curve.AddKey(0.8f, 0.1f);
            curve.AddKey(1.0f, 0.4f);

            lineRenderer.widthCurve = curve;


            doAttraction();
            

            }
        


        
        /*
		GlobalScalers Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GlobalScalers>();
		print("Script.largestLink: " + Script.largestLink);
		weightscale = Script.largestLink;


		if (direction == true)
		{
			lineRenderer.startWidth = 0.5f*weight/weightscale;
			lineRenderer.endWidth = 0.1f*weight/weightscale;

		}
		else
		{
			lineRenderer.startWidth = 0.25f*weight/weightscale;
			lineRenderer.endWidth = 0.25f*weight/weightscale;

		}
*/

        //Make line flash between red and green 
        //lineRenderer.material.color = Color.Lerp(colorStart, colorEnd, (Mathf.PingPong(Time.time, 1) / 1));
    }

    void HideLink()
    {
        lineRenderer.material = hiddenmaterial;
        Color HideColor = new Color(UnityEngine.Random.Range(0f, 0.1f), UnityEngine.Random.Range(0f, 0.1f), UnityEngine.Random.Range(0f, 0.1f), 0.1f);
        lineRenderer.startColor = HideColor;
        lineRenderer.endColor = HideColor;
    }

    void HiddenNodeCheck(GameObject node)
    {
        if(node.GetComponent<ColorSpheres>().hide == true)
        {
            HideLink();
        }
    }

    public void CheckHidden()
    {
        HiddenNodeCheck(source);
        HiddenNodeCheck(target);
    }

    public void ChangeColour(){
		lineRenderer.startColor = colorStart;
		lineRenderer.endColor = colorEnd;
	}


    Color MaterialRed(int PaletteNumber)
    {
        int red, green, blue;

        red = Convert.ToInt32((-0.0682*PaletteNumber)+261);
        green = Convert.ToInt32(257*Mathf.Exp(-2.52f*0.001f*PaletteNumber));
        blue = Convert.ToInt32(262 * Mathf.Exp(-2.61f * 0.001f * PaletteNumber));
        
        if (red>255)
        {
            red = 255;
        }
        if (red < 0)
        {
            red = 0;
        }
        if (green > 255)
        {
            green = 255;
        }
        if (green < 0)
        {
            green = 0;
        }
        if (blue > 255)
        {
            blue = 255;
        }
        if (blue < 0)
        {
            blue = 0;
        }
        
        print(red + " " + green + " " + blue + " Colours!");

        return new Color(red/255f, green/255f, blue/255f, 1f);
    }

}
