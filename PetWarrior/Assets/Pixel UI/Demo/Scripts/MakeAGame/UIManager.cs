/******************************************************************************************
 * Name: UIManager.cs
 * Created by: Jeremy Voss
 * Created on: 02/20/2019
 * Last Modified: 02/20/2019
 * Description:
 * The UI Manager will serve as our bread and butter for managing the UI.  It utilizes a
 * singleton implementation to ensure that only one instance of it is ever running.  The
 * UI Manager will be responsible for creating our UI objects and managing them.
 ******************************************************************************************/
using UnityEngine;

namespace PixelsoftGames.PixelUI
{
    public class UIManager : MonoBehaviour
    {
        #region Singleton Implementation

        static UIManager _Instance = null;
        public static UIManager Instance { get { return _Instance; } }

        #endregion

        #region Fields & Properties

        [SerializeField]
        [Tooltip("The materials used to color the UI.")]
        Material primaryMaterial = null, subMaterial = null, internalMaterial = null, externalMaterial = null;
        [SerializeField]
        [Tooltip("Sets the material colors for the image UI.")]
        Color primaryColor = Color.white, subColor = Color.white;
        [SerializeField]
        [Tooltip("Sets the material colors for the text UI.")]
        Color internalColor = Color.white, externalColor = Color.white;
        [SerializeField]
        [Tooltip("The default material colors for Pixel UI Images")]
        Color defaultImageColor = Color.white;
        [SerializeField]
        [Tooltip("The default material colors for Pixel UI Text")]
        Color defaultTextColor = new Color(32 / 255f, 32 / 255f, 32 / 255f, 1f);
        [SerializeField]
        [Tooltip("The prefab to use for tooltips.")]
        GameObject tooltipPrefab = null;

        /// <summary>
        /// The active canvas to which we will instantiate new UI elements.
        /// </summary>
        [SerializeField]
        [Tooltip("The primary canvas we will be using.")]
        Canvas canvas = null;
        [SerializeField]
        [Tooltip("The game panel that controls the game.")]
        GameObject gameWindow = null;

        [SerializeField]
        [Tooltip("The prefab used to instantiate action buttons.")]
        GameObject actionButtonPrefab = null;
        [SerializeField]
        [Tooltip("The action panel instance used to contain action buttons.")]
        GameObject actionPanel = null;

        [SerializeField]
        [Tooltip("The exp bar instance used to display experience points.")]
        UIExperienceBar expBarInstance = null;
        /// <summary>
        /// Accessor for getting the EXP bar instance.
        /// </summary>
        public UIExperienceBar GetEXPBar { get { return expBarInstance; } }

        [SerializeField]
        [Tooltip("The health bar instance used to display current health.")]
        UIStatBar healthBarInstance = null;
        /// <summary>
        /// Accessor for getting the health bar instance.
        /// </summary>
        public UIStatBar GetHealthBar { get { return healthBarInstance; } }

        [SerializeField]
        [Tooltip("The energy bar instance used to display current energy.")]
        UIStatBar energyBarInstance = null;
        /// <summary>
        /// Accessor used for getting the energy bar instance.
        /// </summary>
        public UIStatBar GetEnergyBar { get { return energyBarInstance; } }

        [SerializeField]
        [Tooltip("The prefab used to instantiate the end of day summary panel.")]
        GameObject endOfDaySummaryPanelPrefab = null;

        #endregion

        #region Monobehavior Callbacks

        private void Awake()
        {
            if (Instance != null)
                Destroy(this);
            else
            {
                _Instance = this;
                GameManager.LocationChanged += GameManager_LocationChanged;
                GameClock.GameDayEnd += GameClock_GameDayEnd;
            }
        }

        private void Start()
        {
            primaryMaterial.color = primaryColor;
            subMaterial.color = subColor;
            internalMaterial.color = internalColor;
            externalMaterial.color = externalColor;
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                _Instance = null;
                primaryMaterial.color = defaultImageColor;
                subMaterial.color = defaultImageColor;
                internalMaterial.color = defaultTextColor;
                externalMaterial.color = defaultTextColor;

                GameManager.LocationChanged -= GameManager_LocationChanged;
                GameClock.GameDayEnd -= GameClock_GameDayEnd;
            }
        }

        #endregion

        #region Callbacks

        /// <summary>
        /// Called by Game Manager when the player changes locations in the game.
        /// </summary>
        /// <param name="instance"></param>
        private void GameManager_LocationChanged(GameManager instance)
        {
            if (actionPanel == null)
                return;

            foreach (Transform t in actionPanel.transform)
                Destroy(t.gameObject);

            foreach (Actions action in instance.GetCurrentLocation.AvailableActions)
            {
                GameObject actionButtonInstance = Instantiate(actionButtonPrefab, actionPanel.transform, false);
                actionButtonInstance.GetComponent<ActionButton>().SetData(action);
            }
        }

        /// <summary>
        /// Called by the Game Clock when a game day ends.  This event will make the 
        /// UI Manager hide the game panel and spawn the end of day summary panel.
        /// </summary>
        /// <param name="clock"></param>
        private void GameClock_GameDayEnd(GameClock clock)
        {
            HideGamePanel();
            ShowEndOfDaySummary();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Shows the current tooltip instance and sets the tooltip text.
        /// </summary>
        /// <param name="text">The text for the tooltip.</param>
        public void ShowTooltip(string text)
        {
            if (UITooltip.Instance == null)
                Instantiate(tooltipPrefab, canvas.transform, false);

            UITooltip.Instance.SetText(text);
            UITooltip.Instance.gameObject.SetActive(true);
        }

        /// <summary>
        /// Hides the current tooltip instance (if one exists).
        /// </summary>
        public void HideTooltip()
        {
            if (UITooltip.Instance != null)
                UITooltip.Instance.gameObject.SetActive(false);
        }

        /// <summary>
        /// Shows the entire game panel.
        /// </summary>
        public void ShowGamePanel()
        {
            CanvasGroup gamePanelCanvasGroup = gameWindow.GetComponent<CanvasGroup>();
            gamePanelCanvasGroup.interactable = true;
            gamePanelCanvasGroup.blocksRaycasts = true;
            gameWindow.GetComponent<UIFader>().FadeIn();
        }

        /// <summary>
        /// Hides the entire game panel.
        /// </summary>
        public void HideGamePanel()
        {
            CanvasGroup gamePanelCanvasGroup = gameWindow.GetComponent<CanvasGroup>();
            gamePanelCanvasGroup.interactable = false;
            gamePanelCanvasGroup.blocksRaycasts = false;
            gameWindow.GetComponent<UIFader>().FadeOut();
        }

        /// <summary>
        /// Used to show the end of day summary screen.
        /// </summary>
        public void ShowEndOfDaySummary()
        {
            Instantiate(endOfDaySummaryPanelPrefab, canvas.transform, false);
        }

        #endregion
    }
}