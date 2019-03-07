using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class controlSlides : MonoBehaviour
{

	public AnimationCurve curve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
	public float timeTakenDuringLerp = 1f;
	private bool _isLerping;

	private Vector3 _startPosition;
	private Vector3 _endPosition;

	public GameObject slide1;
	public GameObject slide2;
	public GameObject slide3;
	public GameObject slide4;
	public GameObject slide5;

	private Quaternion _startPositionRotation;
	private Quaternion _endPositionRotation;

	private float _timeStartedLerping;

	private int p = 0;

	void StartLerping(Vector3 position,Vector3 rotation)
	{
		_isLerping = true;
		_timeStartedLerping = Time.time;

		//We set the start position to the current position, and the finish to 10 spaces in the 'forward' direction
		_startPosition = transform.position;
		_endPosition = position;

		_startPositionRotation = transform.localRotation;
		_endPositionRotation = Quaternion.Euler(rotation);
	}

	void Update()
	{

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}

		List<Vector3> positions = new List<Vector3>();
		positions.Add(slide1.transform.position);
		positions.Add(slide2.transform.position);
		positions.Add(slide3.transform.position);
		positions.Add(slide4.transform.position);
		positions.Add(slide5.transform.position);

		List<Vector3> rotations = new List<Vector3>();
		rotations.Add(new Vector3(26.0f,-51.0f,0.0f));
		rotations.Add(new Vector3(141.0f,66.0f,-60.0f));
		rotations.Add(new Vector3(-105.0f,58.0f,39.0f));
		rotations.Add(new Vector3(50.0f,200.0f,59.0f));
		rotations.Add(new Vector3(50.0f,200.0f,59.0f));

		//if(Input.GetMouseButtonDown(0) || Input.GetButtonDown("L1"))
		if(Input.GetButtonDown("L1"))
		{
			p = p + 1;
			if (p > positions.Count-1) { p = 0; };
			StartLerping(positions[p],rotations[p]);
		}

		//if(Input.GetMouseButtonDown(1) || Input.GetButtonDown("R1"))
		if(Input.GetButtonDown("R1"))
		{
			p = p - 1;
			if (p < 0) { p = positions.Count-1; };
			StartLerping(positions[p],rotations[p]);
		}

		if (_isLerping) {
			float timeSinceStarted = Time.time - _timeStartedLerping;
			float percentageComplete = timeSinceStarted / timeTakenDuringLerp;

			transform.position = Vector3.Lerp(_startPosition, _endPosition, curve.Evaluate(percentageComplete));

			//transform.localRotation = Quaternion.Lerp(_startPositionRotation, _endPositionRotation, curve.Evaluate(percentageComplete));

			if (percentageComplete >= 1.0f) {
				_isLerping = false;
				//transform.localRotation = _endPositionRotation;
			}
		}
	}
}