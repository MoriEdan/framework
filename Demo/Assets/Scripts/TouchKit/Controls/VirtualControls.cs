﻿using Assets.Scripts.TouchKit.Helpers;
using Assets.Scripts.TouchKit.Recognizers;
using UnityEngine;

namespace Assets.Scripts.TouchKit.Controls
{
    /// <summary>
    ///     this class shows one way to setup a virtual control setup (d-pad and two buttons). It illustrates a bunch of
    ///     concepts
    ///     including recognizer layering (overlapping "up" button to get diagonals) and also how to detect only in this frame
    ///     if a button is down (attack button).
    /// </summary>
    public class VirtualControls
    {
        public bool leftDown, upDown, rightDown, attackDown, jumpDown;

        private readonly float buttonWidth = 24f;
        private readonly float buttonHeight = 30f;


        private TKAnyTouchRecognizer _leftRecognizer;
        private TKAnyTouchRecognizer _rightRecognizer;
        private TKAnyTouchRecognizer _upRecognizer;

        private TKButtonRecognizer _attackRecognizer;
        private TKButtonRecognizer _jumpRecognizer;

        public TKRect leftRect
        {
            get { return _leftRecognizer.boundaryFrame.Value; }
        }

        public TKRect rightRect
        {
            get { return _rightRecognizer.boundaryFrame.Value; }
        }

        public TKRect upRect
        {
            get { return _upRecognizer.boundaryFrame.Value; }
        }

        public TKRect attackRect
        {
            get { return _attackRecognizer.boundaryFrame.Value; }
        }

        public TKRect jumpRect
        {
            get { return _jumpRecognizer.boundaryFrame.Value; }
        }


        public VirtualControls()
        {
            // if we have something like the iPad with a squarish aspect ratio adjust the button height to be a bit smaller
            if (Camera.main.aspect < 1.7f)
            {
                buttonHeight *= Camera.main.aspect/1.7f;
                buttonWidth *= Camera.main.aspect/1.7f;
            }
            setupRecognizers();
        }


        public void update()
        {
            // reset our state
            leftDown = upDown = rightDown = attackDown = jumpDown = false;

            // first update our touches then use our recognizers to set our state
            TouchKit.updateTouches();

            leftDown = _leftRecognizer.state == TKGestureRecognizerState.RecognizedAndStillRecognizing;
            rightDown = _rightRecognizer.state == TKGestureRecognizerState.RecognizedAndStillRecognizing;
            upDown = _upRecognizer.state == TKGestureRecognizerState.RecognizedAndStillRecognizing;

            jumpDown = _jumpRecognizer.state == TKGestureRecognizerState.RecognizedAndStillRecognizing;
        }

        private void setupRecognizers()
        {
            // left button
            _leftRecognizer = new TKAnyTouchRecognizer(new TKRect(0f, 0f, buttonWidth, buttonHeight));
            TouchKit.addGestureRecognizer(_leftRecognizer);


            // right button
            _rightRecognizer = new TKAnyTouchRecognizer(new TKRect(buttonWidth + 1f, 0f, buttonWidth, buttonHeight));
            TouchKit.addGestureRecognizer(_rightRecognizer);


            // up button. we place it 70% of the way up the other two buttons allowing some overlap
            _upRecognizer = new TKAnyTouchRecognizer(new TKRect(0f, buttonHeight*0.7f, buttonWidth*2f + 1f, 20f));
            TouchKit.addGestureRecognizer(_upRecognizer);


            // attack button. we use the onSelectedEvent here because we only want to know the exact frame it was pressed
            _attackRecognizer =
                new TKButtonRecognizer(
                    new TKRect(TouchKit.instance.designTimeResolution.x - buttonWidth*2f, 0, buttonWidth, buttonHeight),
                    0f);
            _attackRecognizer.onSelectedEvent += r => { attackDown = true; };
            TouchKit.addGestureRecognizer(_attackRecognizer);


            // jump button
            _jumpRecognizer =
                new TKButtonRecognizer(
                    new TKRect(TouchKit.instance.designTimeResolution.x - buttonWidth, 0, buttonWidth, buttonHeight), 0f);
            TouchKit.addGestureRecognizer(_jumpRecognizer);
        }
    }
}