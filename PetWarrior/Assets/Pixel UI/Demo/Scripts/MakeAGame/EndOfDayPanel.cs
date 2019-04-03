/******************************************************************************************
 * Name: EndOfDayPanel.cs
 * Created by: Jeremy Voss
 * Created on: 02/20/2019
 * Last Modified: 02/27/2019
 * Description:
 * This class manages the effects and scoring used in the end of day panel.  It scores
 * based on money earned for the day minus money spent.  If the total is greater than
 * or equal to the previous highest total, the day is rated 3 gold stars and the the 
 * highest total in the player data is updated.  If no money was earned or negative money 
 * earned (by spending), it will rate the player a single bronze star.  Lastly, if the
 * total was half or greater than the highest total the player is rated 2 silver stars.
 ******************************************************************************************/
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PixelsoftGames.PixelUI
{
    public class EndOfDayPanel : MonoBehaviour
    {
        #region Fields & Properties

        [SerializeField]
        [Tooltip("Sprites associated with the different star colors.")]
        Sprite bronzeStar = null, silverStar = null, goldStar = null;
        [SerializeField]
        [Tooltip("The images associated with displaying the star scores.")]
        Image star1 = null, star2 = null, star3 = null;
        [SerializeField]
        [Tooltip("The label used to summarize money spent vs. money earned and total.")]
        Text moneyEarnedLabel = null, moneySpentLabel = null, totalLabel = null;
        [SerializeField]
        [Tooltip("The amount of time before the score labels must finish counting.")]
        float timeToDisplay = 5f;

        #endregion

        #region Monobehavior Callbacks

        private void Start()
        {
            int earned = GameManager.Instance.GetPlayerData.GetMoneyEarned;
            int spent = GameManager.Instance.GetPlayerData.GetMoneySpent;
            int total = earned - spent;
            int highest = GameManager.Instance.GetPlayerData.HighScore;

            StartCoroutine(Count(earned, moneyEarnedLabel));
            StartCoroutine(Count(spent, moneySpentLabel));
            StartCoroutine(Count(total, totalLabel));

            if (total >= highest)
            {
                star1.sprite = goldStar;
                star1.color = Color.white;
                star2.sprite = goldStar;
                star2.color = Color.white;
                star3.sprite = goldStar;
                star3.color = Color.white;
            }
            else if (total <= 0)
            {
                star1.sprite = bronzeStar;
                star1.color = Color.white;
            }
            else if (total >= highest / 2)
            {
                star1.sprite = silverStar;
                star1.color = Color.white;
                star2.sprite = silverStar;
                star2.color = Color.white;
            }
            else
            {
                star1.sprite = bronzeStar;
                star1.color = Color.white;
            }

            if(total > highest)
                GameManager.Instance.GetPlayerData.HighScore = total;
        }

        #endregion

        #region Callbacks

        /// <summary>
        /// Called when the Continue button is clicked to resume the game.
        /// </summary>
        public void OnClick()
        {
            UIManager.Instance.ShowGamePanel();
            GameManager.Instance.ContinueGame();
            GetComponent<UIFader>().FadeOut();
        }

        #endregion

        #region Coroutines

        /// <summary>
        /// Counts an int label starting at 0 and ending at amount over
        /// a specified duration of time.
        /// </summary>
        /// <param name="amount">The target amount to count to.</param>
        /// <param name="target">The target label to set the counting value to.</param>
        /// <returns></returns>
        IEnumerator Count(int amount, Text target)
        {
            float timer = 0;

            float currentValue = 0;
            while(timer < timeToDisplay)
            {
                currentValue = Mathf.Lerp(0, amount, timer / timeToDisplay);
                currentValue = (int)Mathf.Clamp(currentValue, 0, amount);
                target.text = string.Format("{0:n0}", currentValue);
                timer += Time.deltaTime;
                yield return null;
            }

            target.text = string.Format("{0:n0}", amount);
        }

        #endregion
    }
}