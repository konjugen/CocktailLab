package md5a3b3815015c753f97eb6610db9f5b664;


public class CocktailsContentActivity
	extends md503d368792e76997a708abd586ec33284.BaseActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("CocktailLab.Activities.CocktailsContentActivity, CocktailLab, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CocktailsContentActivity.class, __md_methods);
	}


	public CocktailsContentActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == CocktailsContentActivity.class)
			mono.android.TypeManager.Activate ("CocktailLab.Activities.CocktailsContentActivity, CocktailLab, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
