using Property;
using Tree;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TreeItemUI : MonoBehaviour
    {
        public TreeItemSobj sobj;
        private TreeItemWrapper wrapper;

        public Button btn;
        public Sprite[] boarderSprites;
        Image boarder;
        Image image;
        Text text;
        Sprite mainImage;
        public Text mainDescription;
        public Text SubDescription;
        public Text Cost;

        private void Awake()
        {
            boarder = transform.Find("Border").GetComponent<Image>();
            image = transform.Find("Image").GetComponent<Image>();
            text = transform.Find("ReadableName").GetComponent<Text>();

            //btn = GameObject.FindGameObjectWithTag("UnlockButton").GetComponent<Button>();
        }

        private void Start()
        {
            SetCurrSobj(sobj);
        }

        public void SetCurrSobj(TreeItemSobj newSobj)
        {
            sobj = newSobj;
            wrapper = sobj.wrapper;

            text.text = sobj.readableName;
            mainImage = sobj.mainImage;

            image.sprite = mainImage;

            //ToolTip
            mainDescription.text = sobj.mainDescription;
            SubDescription.text = sobj.subDescription;
            Cost.text = "人口{"+sobj.unlockCost[PropertyType.Population]+"}资产{"+ sobj.unlockCost[PropertyType.Finance] + "}";

            //unlock
            var unlocked = wrapper.Unlocked;
            if (unlocked)
            {
                boarder.sprite = boarderSprites[2]; // 解锁了的边框
            }
            else
            {
                boarder.sprite = boarderSprites[0]; // 原来的边框
            }

            btn.interactable = wrapper.CanUnlock();
            btn.onClick.AddListener(delegate {
                wrapper.TryUnlock();
                boarder.sprite = boarderSprites[2]; // 解锁了的边框
            });
        }



        
    }
}
