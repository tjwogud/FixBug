using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FixBug.Patch
{
    [HarmonyPatch(typeof(scnEditor), "ObjectsAtMouse")]
    public static class ObjectsAtMousePatch
    {
        public static bool Prefix(ref GameObject[] __result, ref GameObject[] ___foundObjects, ref int ___lastFrameUpdated, LayerMask ___floorLayerMask)
        {
            if (Time.frameCount != ___lastFrameUpdated)
            {
                ___lastFrameUpdated = Time.frameCount;
                Vector3 mousePosition = Input.mousePosition;
                Vector2 vector = scrCamera.instance.camobj.ScreenToWorldPoint(mousePosition).xy();
                float magnitude = new Vector2(0.75f, 0.45f).magnitude;
                List<Collider2D> list = new List<Collider2D>();
                List<GameObject> list2 = new List<GameObject>();
                foreach (scrFloor scrFloor in scnEditor.instance.lm.listFloors)
				{
					if (Vector2.Distance(scrFloor.transform.position.xy(), vector) <= magnitude)
					{
						FloorMesh component = scrFloor.GetComponent<FloorMesh>();
						if (component != null)
						{
							component.Method("GenerateCollider");
							if (component.polygonCollider == null)
								component.polygonCollider = (scrFloor.floorRenderer as FloorMeshRenderer).polygonCollider;
							component.polygonCollider.enabled = true;
							list.Add(component.polygonCollider);
						}
						if (scrFloor.GetComponent<FloorSpriteRenderer>() != null)
						{
							GameObject gameObject = scrFloor.Method<GameObject>("GenerateCollider");
							gameObject.name = "Parent";
							list.Add(gameObject.GetComponent<Collider2D>());
							list2.Add(gameObject);
						}
					}
				}
				RaycastHit2D[] array = Physics2D.RaycastAll(vector, Vector2.zero, 0f, ___floorLayerMask);
				if (array == null || array.Length == 0)
				{
					___foundObjects = null;
				}
				else
				{
					___foundObjects = new GameObject[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						Collider2D collider = array[i].collider;
						___foundObjects[i] = ((collider.name == "Parent") ? collider.transform.parent.gameObject : collider.gameObject);
					}
					Array.Sort(___foundObjects, (GameObject x, GameObject y) => y.GetComponentInParent<Renderer>().sortingOrder.CompareTo(x.GetComponentInParent<Renderer>().sortingOrder));
				}
				foreach (Collider2D collider2D in list)
				{
					collider2D.enabled = false;
				}
				foreach (GameObject obj in list2)
				{
					UnityEngine.Object.DestroyImmediate(obj);
				}
			}
			__result = ___foundObjects;
			return false;
		}
    }
}
