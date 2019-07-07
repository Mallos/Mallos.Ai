﻿using System;
using Microsoft.Xna.Framework;
using GoRogue;
using SadConsole;

namespace GoRogueSample
{
    enum MapLayer
    {
        TERRAIN,
        ITEMS,
        MONSTERS,
        PLAYER
    }

    class ExampleMap : BasicMap
    {
        // Handles the changing of tile/entity visiblity as appropriate based on FOV.
        private FOVVisibilityHandler _fovVisibilityHandler;

        public ExampleMap(int width, int height)
            // Allow multiple items on the same location only on the items layer.  This example uses 8-way movement, so Chebyshev distance is selected.
            : base(width, height, Enum.GetNames(typeof(MapLayer)).Length - 1, Distance.CHEBYSHEV, entityLayersSupportingMultipleItems: LayerMasker.DEFAULT.Mask((int)MapLayer.ITEMS))
        {
            _fovVisibilityHandler = new DefaultFOVVisibilityHandler(this, Color.DarkGray);
        }
    }
}
