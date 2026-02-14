using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;

public class DiscordManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static DiscordManager Instance;
    Discord.Discord discord;
    [SerializeField]
    private long appId;


    [Header("Details")]
    [SerializeField] private ActivityType type;
    [SerializeField] private string details;
    [SerializeField] private string state;

    [SerializeField] private string largeImage;
    [SerializeField] private string largeText;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        discord = new Discord.Discord(appId, (ulong)CreateFlags.NoRequireDiscord);

        ChangeActivity(type, details, state, "");
    }
    private void OnDisable()
    {
        discord.Dispose();
    }
    public void ChangeActivity(ActivityType type, string details, string state, string smallImage)
    {
        ActivityManager manager = discord.GetActivityManager();
        Activity activity = new Activity();
        activity.State = state;
        activity.Type = type;
        activity.Details = details;
        activity.Assets.LargeImage = largeImage;
        activity.Assets.LargeText = largeText;
        activity.Assets.SmallImage = smallImage;

        manager.UpdateActivity(activity, (res) =>
        {
            Debug.Log("Richpresence state: " + res);
        });
    }
    // Update is called once per frame
    void Update()
    {
        discord.RunCallbacks();
    }
}
