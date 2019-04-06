using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroloopCubeMove : MonoBehaviour
{
    public float moveSpeed;
    private bool forward, backward, left, right;

    public void Forward()
    {
        forward = true;
    }

    public void ForwardUp()
    {
        forward = false;
    }

    public void Backward()
    {
        backward = true;
    }

    public void BackwardUp()
    {
        backward = false;
    }

    public void Left()
    {
        left = true;
    }

    public void LeftUp()
    {
        left = false;
    }

    public void Right()
    {
        right = true;
    }

    public void RightUp()
    {
        right = false;
    }

	public void Update()
	{
        Vector3 pos = transform.position;
		if(forward)
		{
			pos.z += moveSpeed;
		}
		if(backward)
		{
			pos.z -= moveSpeed;
		}
		if(left)
		{
			pos.x -= moveSpeed;
		}
		if(right)
		{
			pos.x += moveSpeed;
		}
        transform.position = pos;
	}

}
