using UnityEngine;
using System.Collections;

public interface IEntity
{
	void initialize();

    void release();

    void updateFixed();

    void update();

    void updateLate();

    void onEnter();

    void onExit();
}