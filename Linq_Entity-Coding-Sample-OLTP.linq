<Query Kind="Program">
  <Connection>
    <ID>f82afac0-3050-40c9-b69d-e7aa1885402e</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <Server>.</Server>
    <Database>Chinook2018</Database>
    <DisplayName>Chinook-Entity</DisplayName>
    <DriverData>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>

void Main()
{
	try
	{
		//  Post
		#region Query
		//  query method using linq to entity
		//  Track_FetchTrackBy
		//  TrackService is our BLL class name
		//	FetchTrackBy is our method name

		string searchPattern = "deep";
		string searchType = "Artist";

		List<TrackSelection> trackList_display =
										TrackService_FetchTrackBy(searchType, searchPattern);
		//trackList_display.Dump();

		//	PlaylistTrackService is the BLL class name
		//  FetchPlaylist is the method call

		string playlistName = "hansenb1";
		string username = "HansenB";

		List<PlaylistTrackInfo> playlist_display =
				PlaylistTrackService_FetchPlaylist(playlistName, username);
		//playlist_display.Dump();
		#endregion

		#region Command method using Linq to Entity
		//	793 A Castle Full of Rascals
		//	822	A Twist In The Tail
		//	543	Burn
		//	756	Child in Time

		playlistName = "hansenb1";
		int trackID = 822;

		//PlaylistTrackService_AddTrack(playlistName, username, trackID);

		//	On the webpage, the post method would have already have access to the
		//  BindProperty variables containing the input values
		playlistName = "hansenbtest";
		List<PlaylistTrackTRX> tracklistInfo = new List<PlaylistTrackTRX>();
		tracklistInfo.Add(new PlaylistTrackTRX()
		{
			SelectedTrack = false,
			TrackID = 543,
			TrackNumber = 1,
			TrackInput = 6
		});
		tracklistInfo.Add(new PlaylistTrackTRX()
		{
			SelectedTrack = false,
			TrackID = 756,
			TrackNumber = 2,
			TrackInput = 99
		});
		tracklistInfo.Add(new PlaylistTrackTRX()
		{
			SelectedTrack = true,
			TrackID = 822,
			TrackNumber = 3,
			TrackInput = 8
		});
		tracklistInfo.Add(new PlaylistTrackTRX()
		{
			SelectedTrack = true,
			TrackID = 793,
			TrackNumber = 4,
			TrackInput = 2
		});

		//	Call the service method to process data deletion
		PlayListTrackService_RemoveTracks(playlistName, username, tracklistInfo);

		//	call the service method to process the data
		//PlaylistTrack_MoveTracks(playlistName, username, tracklistInfo)


		#endregion
	}
	catch (AggregateException ex)
	{
		foreach (var error in ex.InnerExceptions)
		{
			error.Message.Dump();
		}
	}
	catch (ArgumentNullException ex)
	{
		GetInnerException(ex).Message.Dump();
	}

	catch (Exception ex)
	{
		GetInnerException(ex).Message.Dump();
	}
}
// You can define other methods, fields, classes and namespaces here

#region Methods
private Exception GetInnerException(Exception ex)
{
	while (ex.InnerException != null)
		ex = ex.InnerException;
	return ex;
}
#endregion

#region Models
public class TrackSelection
{
	public int TrackId { get; set; }
	public string SongName { get; set; }
	public string AlbumTitle { get; set; }
	public string ArtistName { get; set; }
	public int Milliseconds { get; set; }
	public decimal Price { get; set; }
}

public class PlaylistTrackInfo
{
	public int TrackId { get; set; }
	public int TrackNumber { get; set; }
	public string SongName { get; set; }
	public int Milliseconds { get; set; }
}

public class PlaylistTrackTRX
{
	public bool SelectedTrack { get; set; }
	public int TrackID { get; set; }
	public int TrackNumber { get; set; }
	public int TrackInput { get; set; }
}
#endregion

#region TrackService Class
#region Query
public List<TrackSelection> TrackService_FetchTrackBy(string searchType,
												string searchPattern)
{
	IEnumerable<TrackSelection> tracks = Tracks
	.Where(x => searchType.Equals("Artist") ?
								x.Album.Artist.Name.Contains(searchPattern) :
								x.Album.Title.Contains(searchPattern))
				.Select(x => new TrackSelection
				{
					TrackId = x.TrackId,
					SongName = x.Name,
					AlbumTitle = x.Album.Title,
					ArtistName = x.Album.Artist.Name,
					Milliseconds = x.Milliseconds,
					Price = x.UnitPrice
				});

	return tracks.ToList();
}
#endregion
#endregion

#region PlaylistTrack Service
#region Query
public List<PlaylistTrackInfo> PlaylistTrackService_FetchPlaylist(string playlistName, string username)
{
	IEnumerable<PlaylistTrackInfo> playlist = PlaylistTracks
									.Where(x => x.Playlist.Name == playlistName
									&& x.Playlist.UserName == username)
									.Select(x => new PlaylistTrackInfo
									{
										TrackId = x.TrackId,
										TrackNumber = x.TrackNumber,
										SongName = x.Track.Name,
										Milliseconds = x.Track.Milliseconds
									})
									.OrderBy(x => x.TrackNumber);
	return playlist.ToList();
}
#endregion
#endregion

#region Commands

public void PlaylistTrackService_AddTrack(string playlistName, string username, int trackID)
{
	//  local variables
	bool trackExist = false;
	Playlists playlist = null;
	int trackNumber = 0;
	PlaylistTracks playlistTrackExist = null;

	#region Business Logic and Parameter Exceptions
	List<Exception> errorList = new List<Exception>();

	//  Business Rules
	//	There are processing rules that need to be satisfied for valid data.
	//		rule:	a track can only exist once on a playlist
	//		rule:	each track on a playlist is assigned a continous (sequential) track number
	//
	//	If the business rules are passed, consider the data valid, then
	//		a)	stage your transaction work (Adds, Updates or Deletes)
	//		b)	execute a SINGLE .SaveChanges() - commits to database

	//	We could assume that user name and track ID will always be valid (James)

	//  parameter validation
	if (string.IsNullOrWhiteSpace(playlistName))
	{
		throw new ArgumentNullException("Playlist name is missing");
	}
	if (string.IsNullOrWhiteSpace(username))
	{
		throw new ArgumentNullException("User name is missing");
	}
	#endregion
	//  check that the incoming data exists
	trackExist = Tracks
					.Where(x => x.TrackId == trackID)
					.Any();
	if (!trackExist)
	{
		throw new ArgumentNullException("Selected track no longer is on file.  Refresh track table");
	}

	//  Business Rules
	//  Check if playlist exists.
	playlist = Playlists
				.Where(x => x.Name == playlistName &&
						x.UserName == username)
						.FirstOrDefault();
	//  does not exist
	if (playlist == null)
	{
		playlist = new Playlists
		{
			Name = playlistName,
			UserName = username
		};
		//  stage (only in memory)
		Playlists.Add(playlist);
		trackNumber = 1;
	}
	else
	{
		playlistTrackExist = PlaylistTracks
							.Where(x => x.TrackId == trackID &&
							x.PlaylistId == playlist.PlaylistId)
							.FirstOrDefault();
		if (playlistTrackExist != null)
		{
			var songName = Tracks
							.Where(x => x.TrackId == trackID)
							.Select(x => x.Name);
			errorList.Add(new Exception($"Selected track ({songName}) is already on the playlist"));
		}
		else
		{
			trackNumber = PlaylistTracks
							.Where(x => x.PlaylistId == playlist.PlaylistId)
							.Count() + 1;
		}
	}

	//  add the track to the playlist
	//  create an instance for the playlist track

	playlistTrackExist = new PlaylistTracks();

	//  load values
	playlistTrackExist.TrackId = trackID;
	playlistTrackExist.TrackNumber = trackNumber;

	//  What about the second part of the primary key:  PlaylistID?
	//	If the playlist exists, then we know the id:  playlistExists.PlaylistID
	//	But if the playlis is new, we DO NOT know the ID.

	//  In the situation of a NEW playlist, even though we have created the
	//		playlist instance (se above) it is ONLY staged!!!
	//	This means that the actual sql record has NOT yet been created.
	//	This means that the IDENITY value for the new playlist DOES NOT YET EXIST,
	//	The vaoule of the playlist instance (playlistExist) is zero (0)
	//	Therefore, we have a serious problem.

	//	Solution
	//	It is built into the Entity Framework software and is based using the
	//		navigational property in the PlayList pointing to it's "child".

	//	Staging a typical Add in the past was to reference the entity and
	//		use the entity.Add(xxx).
	//			_context.PlaylistTrack.add(playlistTrackExists)
	//	If you use this statement the playlistID would be zero (0)
	//		causing your transaction to abort.
	//	WHY.	PKeys cannot be zero (0) (FKey to PKey problem)

	//  INSTEAD, do the staging using the "parent.navChildProperty.Add(xxx)
	playlist.PlaylistTracks.Add(playlistTrackExist);

	//	Staging is completed
	//	Commit the work (Transaction)
	//	Committing the work needs a .SaveChanges()
	//	A transaction has ONLY ONE .SaveChanges()
	//	BUT what if you have discovered errors during the business process???
	//	IF so, then throw all errors and DO NOT COMMIT!!!
	if (errorList.Count > 0)
	{
		//  throw the list of bussiness processing error(s)
		throw new AggregateException("Unable to add new track.  Check concerns", errorList);
	}
	else
	{
		//  consider data valid
		//  has passed business processing rules
		SaveChanges();
	}
}

public void PlayListTrackService_RemoveTracks(string playlistName, string username,
												List<PlaylistTrackTRX> tracklistInfo)
{
	//  local variables
	Playlists playlistExist = null;
	PlaylistTracks playListTrackExist = null;
	int trackNumber = 0;

	//	We need a container to hold x number of exception messages
	List<Exception> errorList = new List<Exception>();

	//  parameter validation
	if (string.IsNullOrWhiteSpace(playlistName))
	{
		throw new ArgumentNullException("Playlist name is missing");
	}
	if (string.IsNullOrWhiteSpace(username))
	{
		throw new ArgumentNullException("User name is missing");
	}

	int count = tracklistInfo.Count();
	if (count == 0)
	{
		throw new ArgumentNullException("No list of tracks were submitted");
	}
	else
	{
		//	1) Obtain the tracks to keep
		//	2) The SelectedTrack is a boolean field
		//		false:	keep it
		//		true:	remove ite
		//	3) Create a query to extract the "keep" tracks from the incoming data.
		IEnumerable<PlaylistTrackTRX> keepList = tracklistInfo
												//.Where(x => x.SelectedTrack == false)
												.Where(x => !x.SelectedTrack)
												.OrderBy(x => x.TrackNumber);




		//  obtain the tracks to remove
		IEnumerable<PlaylistTrackTRX> removeList = tracklistInfo
													.Where(x => x.SelectedTrack);

		foreach (PlaylistTrackTRX item in removeList)
		{
			playListTrackExist = PlaylistTracks
							.Where(x => x.Playlist.Name.Equals(playlistName)
								&& x.Playlist.UserName.Equals(username)
								&& x.TrackId == item.TrackID)
								.FirstOrDefault();

			if (playListTrackExist != null)
			{
				PlaylistTracks.Remove(playListTrackExist);
			}

		}
		trackNumber = 1;
		foreach (PlaylistTrackTRX item in keepList)
		{
			playListTrackExist = PlaylistTracks
							.Where(x => x.Playlist.Name.Equals(playlistName)
								&& x.Playlist.UserName.Equals(username)
								&& x.TrackId == item.TrackID)
								.FirstOrDefault();
			if (playListTrackExist != null)
			{
				playListTrackExist.TrackNumber = trackNumber;
				PlaylistTracks.Update(playListTrackExist);

				//  This library is not directly accessable by linqpad
				//	EntityEntry<PlaylistTracks> updating = _context.Entry(playlistTracks)
				//	updating.state = Mircsoft.EntityFrameworkCore.EntityState.Modify;

				//	Get ready for next track
				trackNumber++;
			}
			else
			{
				var songName = Tracks
								.Where(x => x.TrackId == item.TrackID)
								.Select(x => x.Name)
								.SingleOrDefault();
				errorList.Add(new Exception($"The track ({songName}) is no longer on file.  Please remove"));
			}
		}

		if (errorList.Count() > 0)
		{
			throw new AggregateException("Unable to remove request tracks.  Check concern.", errorList);
		}
		else
		{
			SaveChanges();
		}

	}
}

public void PlaylistTrack_MoveTracks(string playlistName, string username,
												List<PlaylistTrackTRX> tracklistInfo)
{
	//  local variables
	Playlists playlistExist = null;
	PlaylistTracks playlistTrackExist = null;
	int trackNumber = 0;

	//	We need a container to hold x number of exception messages
	List<Exception> errorList = new List<Exception>();

	//  parameter validation
	if (string.IsNullOrWhiteSpace(playlistName))
	{
		throw new ArgumentNullException("Playlist name is missing");
	}
	if (string.IsNullOrWhiteSpace(username))
	{
		throw new ArgumentNullException("User name is missing");
	}

	int count = tracklistInfo.Count();
	if (count == 0)
	{
		throw new ArgumentNullException("No list of tracks were submitted");
	}
	playlistExist = Playlists
						.Where(x => x.Name.Equals(playlistName)
								&& x.UserName.Equals(username))
								.Select(x => x)
								.FirstOrDefault();

	if (playlistExist == null)
	{
		errorList.Add(new Exception($"Play list {playlistName} does not exist for this user."));
	}
	else
	{
		//	validation loop to check that the data is indeed a positive number
		//	Use int.TryParse to check that the value to be tested is a number
		//	Check the reuslt of TryParse against value of 1;

		int tempNum = 0;
		foreach (var track in tracklistInfo)
		{
			var songname = Tracks
							.Where(x => x.TrackId == track.TrackID)
							.Select(x => x.Name)
							.SingleOrDefault();
			if (int.TryParse(track.TrackInput.ToString(), out tempNum))
			{
				if (tempNum < 1)
				{
					errorList.Add(new Exception($"The track {songname} re-sequence value needs to be greater than 0.  Example: 3"));
				}
			}
			else
			{
				errorList.Add(new Exception($"The track {songname} re-sequence value needs to be a number.  Example: 3"));
			}
		}

		//	Sort the command model data list on the re-org value
		//	in ascending order comparing x to y;
		//	in descending order	comparing y to x
		tracklistInfo.Sort((x, y) => x.TrackInput.CompareTo(y.TrackInput));

		//	b)	unique new track numbers
		//	The collection has been sorted in ascending order therefore the next
		//		number must be equal to or greater thant he previous number.
		//	One could check to see if the enxt number is +1 of the previous number
		//		BUT rhe re-org look which does the actual re-sequence of numbers
		//		will have that situation.
		//		Therefore "holes" in the loop does not matter (logically)

		for (int i = 0; i < tracklistInfo.Count() - 1; i++)
		{
			var songname1 = Tracks
							.Where(x => x.TrackId == tracklistInfo[i].TrackID)
							.Select(x => x.Name)
							.SingleOrDefault();

			var songname2 = Tracks
				.Where(x => x.TrackId == tracklistInfo[i + 1].TrackID)
				.Select(x => x.Name)
				.SingleOrDefault();
			if (tracklistInfo[i].TrackInput == tracklistInfo[i + 1].TrackInput)
			{
				errorList.Add(new Exception($"{songname1} and {songname2} have the same re-sequence value.  Re-sequence numbers must be unique"));
			}

		}

		trackNumber = 1;
		foreach (PlaylistTrackTRX item in tracklistInfo)
		{
			playlistTrackExist = PlaylistTracks
									.Where(x => x.Playlist.Name.Equals(playlistName)
									&& x.Playlist.UserName.Equals(username)
									&& x.TrackId == item.TrackID)
									.FirstOrDefault();
			if (playlistTrackExist != null)
			{
				playlistTrackExist.TrackNumber = trackNumber;
				PlaylistTracks.Update(playlistTrackExist);

				//  This library is not directly accessable by linqpad
				//	EntityEntry<PlaylistTracks> updating = _context.Entry(playlistTracks)
				//	updating.state = Mircsoft.EntityFrameworkCore.EntityState.Modify;

				//	Get ready for next track
				trackNumber++;
			}
			else
			{
				var songName = Tracks
								.Where(x => x.TrackId == item.TrackID)
								.Select(x => x.Name)
								.SingleOrDefault();
				errorList.Add(new Exception($"The track ({songName}) is no longer on file.  Please remove"));
			}
		}

		if (errorList.Count() > 0)
		{
			throw new AggregateException("Unable to remove request tracks.  Check concern.", errorList);
		}
		else
		{
			SaveChanges();
		}

	}
}

#endregion

















