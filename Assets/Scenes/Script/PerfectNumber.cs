using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PerfectNumber : MonoBehaviour
{
    public GameObject textInput, childText, childPanel;
    public float Q;
    public float[] N;
    
    public void Generate(){
        //get data from inputField
        Q = float.Parse(textInput.GetComponent<TMP_InputField>().text);
        if(1 <= Q && Q <=50){
            GeneratedN(Q);
        }
        else{
            Debug.Log("Please input number beetween 1-50");
        }
    }

    //generated random number
    void GeneratedN(float Q){
        DeleteSort();
        
        for(int i = 0; i < Q; i++) {
            N[i] = Random.Range(2,1000);
            GetFactors(N[i], i);
        }
    }

    void DeleteSort(){
        Transform[] children = childPanel.GetComponentsInChildren<Transform>();

        // Iterate through all the child objects
        for (int i = 1; i < children.Length; i++)
        {
            // Destroy each child object
            Destroy(children[i].gameObject);
        }
    }

    void GetFactors(float Q, int Count){
        float sum = 0;
        string status;
        Debug.Log("The factors of " + Q + " are: ");

        for(int i = 1; i < Q; i++) {    

                if (Q % i == 0)
                {
                    Debug.Log(i + " ");
                    sum += i;
                }
        }
        Debug.Log("SumFactor = "+ sum);
        
        //check Status
        if(Q==sum){
            status = "PerfectNumber";
            Debug.Log("PerfectNumber");
        }

        else if(System.Math.Abs(Q-sum) == 1){
            status = "Hampir";
            Debug.Log("Hampir");
        }

        else{
            status = "Bukan";
            Debug.Log("Bukan");
        }

        //instantiate text
        var ld = Instantiate (childText, childPanel.transform);

        var childSort = ld.transform.GetChild(0);
        childSort.GetComponent<TextMeshProUGUI>().text = (Count+1).ToString();
        
        var childNumber = ld.transform.GetChild(1);
        childNumber.GetComponent<TextMeshProUGUI>().text = Q.ToString();
        
        var childStatus = ld.transform.GetChild(2);
        childStatus.GetComponent<TextMeshProUGUI>().text = status;
    }
}
