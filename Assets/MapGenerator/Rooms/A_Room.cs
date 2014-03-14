using System;
using System.Collections;

namespace MyNameSpace
{
	public abstract class Room : IComparable<Room>
	{
		int height;
		int maxWidth;
		int maxLength;
		int numRectangles;
		public Point ActualLocation;
		public int level;
		public ArrayList listOfRectangles;
		Random r;

		public Room(int l, int w, int h, int r, Random r_in) {
			height = h;
			maxWidth = w;
			maxLength = l;
			numRectangles = r;
			listOfRectangles = new ArrayList ();
			this.r = r_in;
			build ();
		}

		public int CompareTo(Room c_in) {
			if ( level > c_in.level ) return 1;
			else return -1;
		}

		public void setLevel(int l) {
			level = l;
		}

		protected virtual void build() {
			int sX, sY, fX, fY;
			for (int i = 0; i < numRectangles; i++) {
				// Build a rectangle
				sX = r.Next(maxWidth-3);
				sY = r.Next(maxLength-3);
				do { fX = r.Next (maxWidth); } while(fX-sX < 3);
				do { fY = r.Next (maxLength); } while(fY-sY < 3);

				Rect tempRect = new Rect(sX,sY, fX, fY);

				if ( listOfRectangles.Count > 0 ){
					bool connected = false;
					foreach( Rect n in listOfRectangles ) {
						// Check to make sure this rectangle intersects at least one other rectangle.
						connected = ( connected || tempRect.intersects(n) );
					}
					
					// If it is not connected, build a new rectangle from scratch, as there is no guarantee it will be connected by the end unless it is done this way.
					if ( !connected ) { i--; continue; }
				}

				listOfRectangles.Add (tempRect);
			}
		}

		public int getMaxWidth() { return maxWidth; }
		public int getMaxLength() { return maxLength; }

	}
}

