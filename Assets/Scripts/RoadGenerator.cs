using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadGenerator : MonoBehaviour
{

	// road chunk prefab
	public GameObject[] roadChunks;
	// distance between edges of the chunk.
	public float chunkLength;

	// number of chunks to be activated at a time.
	public int drawingAmount = 3;

	// reference to player object to manage the chunks
	[SerializeField] private Transform player = null;

	// total number of chunks that actually exist in the scene
	[SerializeField] private int numberOfChunks = 7;

	// list of references to chunks in the scence
	private Queue<GameObject> chunks;

	// reference to chunk that the player is on
	private GameObject currentChunk;
	private int indexOfCurrentChunk;
	private int currentChunkPosition = 0;

	private void Awake()
	{
		InitializeChunksList();
	}

	private void InitializeChunksList()
	{
		chunks = new Queue<GameObject>();
		for (int i = 0; i < numberOfChunks; i++)
		{
			int randomSection = Random.Range(0, roadChunks.Length);
			

			GameObject _chunk = Instantiate<GameObject>(roadChunks[randomSection]);
			_chunk.transform.position = NextChunkPosition(_chunk);
			if (i != 0)
				_chunk.SetActive(false);
			chunks.Enqueue(_chunk);
		}
	}


	private void FixedUpdate()
	{

		if (!player) return;

		// determine the chunk that the player is on
		currentChunk = GetCurrentChunk();
		indexOfCurrentChunk = GetIndexOfCurrentChunk();

		// Manage chunks based on current chunk that the player is on
		for (int i = indexOfCurrentChunk; i < (indexOfCurrentChunk + drawingAmount); i++)
		{
			i = Mathf.Clamp(i, 0, chunks.Count - 1);
			GameObject _chunkGO = (chunks.ToArray()[i]).gameObject;
			if (!_chunkGO.activeInHierarchy)
				_chunkGO.SetActive(true);
		}

		if (indexOfCurrentChunk > 0)
		{
			float _distance = Vector3.Distance(player.position, (chunks.ToArray()[indexOfCurrentChunk - 1]).transform.position);
			if (_distance > (chunkLength * .75f))
				SweepPreviousChunk();
		}

	}

	private void SweepPreviousChunk()
	{
		GameObject _chunk = chunks.Dequeue();
		_chunk.SetActive(false);
		_chunk.transform.position = NextChunkPosition(_chunk);
		_chunk.transform.rotation = NextChunkRotation(_chunk);
		chunks.Enqueue(_chunk);
	}

	private Vector3 NextChunkPosition(GameObject _chunk)
	{
		float _position = currentChunkPosition;
		currentChunkPosition += (int)chunkLength;

		if (_chunk.name == "road_curve(Clone)")
        {
			return new Vector3(_position, 1, 6.5f);
		}
		else if(roadChunks.Length > 1)
		{
			GameObject _prevChunk = GetCurrentChunk();
			if (_prevChunk.name == "road_curve(Clone)")
            {
				return new Vector3(_position, 1, 6.5f);
			}
			else
            {
				return new Vector3(_position, 1, 0);
			}
		}
		else
        {
			return new Vector3(_position, 1, 0);
		}




	}

	private Quaternion NextChunkRotation(GameObject _chunk)
    {

		GameObject _prevChunk = GetCurrentChunk();
		Quaternion spawnRotation = _prevChunk.transform.rotation;


		return spawnRotation;
    }

	private GameObject GetCurrentChunk()
	{
		GameObject current_chunk = null;
		foreach (GameObject c in chunks)
		{
			if (Vector3.Distance(player.position, c.transform.position) <= (chunkLength / 2))
			{
				current_chunk = c;
				break;
			}
		}
		return current_chunk;
	}

	private int GetIndexOfCurrentChunk()
	{
		int index = -1;
		for (int i = 0; i < chunks.Count; i++)
		{
			if ((chunks.ToArray()[i]).Equals(currentChunk))
			{
				index = i;
				break;
			}
		}
		return index;
	}

}
