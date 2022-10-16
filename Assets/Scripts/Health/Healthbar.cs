using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;
    public GameObject HealthPanel;
    public GameObject HealthPanel1 ;
 

    private void Start()
    {
        totalHealthBar.fillAmount = playerHealth.currenthealth / 10;
	  //Time.timeScale=1;
	  //yield return new WaitForSeconds(time);
        //HealthPanel.SetActive(false);
	   
        
    }


    private void Update()
    {
	  /*
	  if (HealthPanel!=null && HealthPanel.activeInHierarchy == false &&  playerHealth.currenthealth==2)

		{
              
               StartCoroutine(ClosePanelDelayed());
		   
		}
	else if (playerHealth.currenthealth==3)
          {
             HealthPanel.SetActive(false);
	    }
	*/
  

    
    	
        currentHealthBar.fillAmount = playerHealth.currenthealth / 10;
    }

    public IEnumerator ClosePanelDelayed() {
     HealthPanel.SetActive(true);
    yield return new WaitForSecondsRealtime (2.0f);    // Wait for 2.3 seconds
   
    HealthPanel.SetActive(false);
     Destroy(HealthPanel);
}
   
}
