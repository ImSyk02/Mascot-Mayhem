﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework; //for draw method
using Microsoft.Xna.Framework.Graphics;

//Author(s): Beau Marwaha, Jared Miller
//Purpose: Represents a team of units in the game
namespace Game1
{
    class College
    {
        // attributes
        // array for units to be loaded
        private Unit[] units;

        // mascot unit to load
        private Mascot mascot;

        // name for the college
        private string collegeName;

        //unit Texture2D's
        private Dictionary<string, Texture2D> unitSprites;

        //default constructor
        public College()
        {
            collegeName = "Empty College";
        }

        //param constructor 
        public College(Unit[] team, Mascot masc, string name)
        {
            units = team;
            mascot = masc;
            collegeName = name;
        }
        
        //properties
        public Unit[] Units
        {
            get { return units; }
        }

        public Mascot Mascot
        {
            get { return mascot; }
        }

        public string CollegeName
        {
            get { return collegeName; }
        }

        //loads predefined college teams
        public void LoadCollege(string name, Dictionary<string, Texture2D> sprites, int team)
        {
            collegeName = name;
            unitSprites = sprites;

            //depending on team name load the predefined team comps
            if(name == "RIT")
            {
                string school = "RIT";
                units = new Unit[10];
                mascot = new Mascot("Ritchie", "Super Pounce", team, school);
                units[0] = new Unit("Football", team, school);
                units[1] = new Unit("Fraternity", team, school);
                units[2] = new Unit("Archery Club", team, school);
                units[3] = new Unit("Hockey", team, school);
                units[4] = new Unit("EMS Club", team, school);
                units[5] = mascot;
                units[6] = new Unit("Hockey", team, school);
                units[7] = new Unit("Outdoor Club", team, school);
                units[8] = new Unit("Sorority", team, school);
                units[9] = new Unit("Football", team, school);
            }
            else if (name == "UofR")
            {
                string school = "UofR";
                units = new Unit[10];
                mascot = new Mascot("Rocky", "Super Slam", team, school);
                units[0] = new Unit("Football", team, school);
                units[1] = new Unit("Sorority", team, school);
                units[2] = new Unit("Outdoor Club", team, school);
                units[3] = new Unit("Lacrosse", team, school);
                units[4] = mascot;
                units[5] = new Unit("EMS Club", team, school);
                units[6] = new Unit("Lacrosse", team, school);
                units[7] = new Unit("Archery Club", team, school);
                units[8] = new Unit("Fraternity", team, school);
                units[9] = new Unit("Football", team, school);
                
            }
        }

        //draws the college units onto the screen
        public void DrawCollegeUnits(SpriteBatch sB, GraphicsDevice gD, int turn)
        {
            foreach(Unit unit in units)
            {
                if (unit.Alive)
                {
                    if(unit.Team == 1) //flip image
                    {
                        var origin = new Vector2(unitSprites[unit.UnitName].Width / 20f, unitSprites[unit.UnitName].Height / 20f); //for use with image flipping
                        sB.Draw(unitSprites[unit.UnitName], new Rectangle(unit.MapX * gD.Viewport.Width / 10, unit.MapY * gD.Viewport.Height / 10, gD.Viewport.Width / 10, gD.Viewport.Height / 10), null, TurnCheck(unit, turn), 0f, origin, SpriteEffects.FlipHorizontally, 0f);
                    }
                    else //don't flip image
                    {
                        sB.Draw(unitSprites[unit.UnitName], new Rectangle(unit.MapX * gD.Viewport.Width / 10, unit.MapY * gD.Viewport.Height / 10, gD.Viewport.Width / 10, gD.Viewport.Height / 10), TurnCheck(unit, turn));
                    }
                }
                
            }
        }

        public void DrawUnitInfo(SpriteBatch spriteBatch, GraphicsDevice gD, int x, int y, SpriteFont font, int drawX)
        {
            foreach (Unit unit in units)
            {
                if(unit.MapX == x && unit.MapY == y && unit.Alive) //if the unit is in the specified position and is alive
                {
                    //draw unit info
                    spriteBatch.DrawString(font, "Unit: " + unit.UnitName, new Vector2(drawX, gD.Viewport.Height - 255), Color.White);
                    spriteBatch.DrawString(font, "Health: " + unit.CurrHealth + "/" + unit.TotalHealth, new Vector2(drawX, gD.Viewport.Height - 230), Color.White);
                    spriteBatch.DrawString(font, "Attack: " + unit.Attack, new Vector2(drawX, gD.Viewport.Height - 205), Color.White);
                    spriteBatch.DrawString(font, "Defense: " + unit.Defense, new Vector2(drawX, gD.Viewport.Height - 180), Color.White);
                    spriteBatch.DrawString(font, "Move Points: " + unit.CurrMovePoints, new Vector2(drawX, gD.Viewport.Height - 155), Color.White);
                    spriteBatch.DrawString(font, "School: " + unit.School, new Vector2(drawX, gD.Viewport.Height - 130), Color.White);
                }
                
            }
                
        }

        //darken units when it isn't their turn or they have already used their move this turn
        public Color TurnCheck(Unit unit, int turn)
        {
            if (unit.Team != turn) //check if its not that unit's team's turn
            {
                return Color.Gray; //darken
            }
            else if (unit.TurnDone) //check if it's that unit's turn but they've already done their action this turn
            {
                return Color.LightGray; //slightly darken
            }
            else if(unit.School == "RIT") //RIT Team Colors
            {
                return Color.Orange; 
            }
            else if(unit.School == "UofR") //UofR Team Colors
            {
                return Color.Aqua; 
            }

            return Color.White;
        }
    }
}
