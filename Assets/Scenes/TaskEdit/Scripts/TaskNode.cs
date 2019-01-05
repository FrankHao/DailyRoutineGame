
namespace KidsTodo.Task
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class TaskNode : MonoBehaviour
    {
        private float distance = 5f;

        [SerializeField]
        protected GameObject taskName;
        public string TaskName
        {
            get
            {
                return taskName.GetComponent<TextMesh>().text;
            }
            set
            {
                taskName.GetComponent<TextMesh>().text = value;
            }
        }

        /// <summary>
        /// On mouse drag function.
        /// </summary>
        private void OnMouseDrag()
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            transform.position = objPosition;
        }

        /// <summary>
        /// On mouse enter function.
        /// </summary>
        private void OnMouseEnter()
        {
            Debug.Log("OnMouseEnter");
        }
    }
}

